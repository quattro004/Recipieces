import React, { Component } from 'react'
import { Formik, Form, useField  } from 'formik';
import * as Yup from 'yup';
import { MyDropZone } from './MyDropZone';

const axios = require('axios').default;
const ALBUMS_API = 'albums/';

export class AlbumForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isLoading: true,
      error: null,
      nameRequired: true,
    };
    this.albumDropZone = React.createRef();
    this.handleCancel = this.handleCancel.bind(this);
    this.onBlur = this.onBlur.bind(this);
  }

 createAlbum(albumData) {
    console.debug('Posting albumData');
    // Create the album first
    //
    axios.post(ALBUMS_API,  {name: albumData.name, description: albumData.description })
      .then(response => {
        // Now post album contents.
        //
        const config = {
          headers: { 'content-type': 'multipart/form-data' }
        }
        let files = albumData.contents;
        let fileCount = files.length;
        let formData = new FormData();
        formData.append("albumId", response.data.id);

        for (let i = 0; i < fileCount; i++) {
          let file = files[i];
          formData.append("contents", file, file.name);
        }
        axios.post(ALBUMS_API + 'UpdateContents', formData, config)  
          .then(response => {
            this.setState({ isLoading: false });
            // TODO: redirect back to albums list?
          }).catch(err => {
            console.error('Failed to upload album contents');
            console.error(err);
            this.setState({ isLoading: false, error: err });
        });
      }).catch(err => {
        console.error('Failed to create album');
        console.error(err);
        this.setState({ isLoading: false, error: err });
    });
  }

  handleCancel() {
    console.debug('handleCancel');
    this.setState({ nameRequired: false });
  }

  onBlur(event) {
    console.debug('onBlur');
    let target = event.target;
    if (target.value !== undefined && target.value.trim() === '') {
      this.setState({ nameRequired: true });
    } else {
      this.setState({ nameRequired: false });
    } 
  }

  render() {
    // TODO: put this into it's own file
    const RecipiecesInput = ({ label, ...props }) => {
    // useField() returns [formik.getFieldProps(), formik.getFieldMeta()]
    // which we can spread on <input> and also replace ErrorMessage entirely.
    
    const [field, meta] = useField(props);
    return (
        <>
          <label htmlFor={props.id || props.name}>{label}</label>
          <input className="form-control" {...field} {...props} />
          {meta.touched && meta.error 
            ? (<div className="text-danger">{meta.error}</div>)
            : null}
        </>
      );
    };

    const { error } = this.state;
    if (error) {
      return <div className="text-danger">{error.message}</div>;
    }
    
    return (
      <div>
        <h1>Create Album</h1>
        <Formik
          initialValues={{ name: '', description: '', contents: []}}
          validationSchema={Yup.object({
            name: Yup.string()
                    .max(50, 'Must be 50 characters or less')
                    .required('Required'),
            })}
          onSubmit={async (values, { setSubmitting }) => {
            console.debug('onSubmit()');
            values.contents = this.albumDropZone.current.state.droppedFiles;
            this.createAlbum(values);
            setSubmitting(false);
          }} >
          {({ values, touched, errors, dirty, isSubmitting }) => (
            <Form>
              <div className="form-row form-group">
                <div className="col-3">
                  <RecipiecesInput 
                    autoFocus="autofocus"
                    name="name"
                    type="text"
                    placeholder="Name"
                    // onBlur={this.onBlur}
                  />
                </div>
                <div className="col">
                  <RecipiecesInput
                    name="description"
                    type="text"
                    placeholder="Description"
                />
                </div>
              </div>
              <div className="form-row col-10">
                <div id="dropZone" className="form-group col-12">
                    <MyDropZone ref={this.albumDropZone} />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group col">
                  {isSubmitting ? (
                    <button name="createAlbumButton" type="submit" className="btn btn-primary">
                      Creating...
                      <span className="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                    </button>
                  ) :
                    <button name="createAlbumButton" type="submit" className="btn btn-primary">
                      Create Album
                        </button>
                  }
                </div>
                <a href="/" onClick={this.handleCancel} >Back to List</a>
              </div>
            </Form>
          )}
        </Formik>
      </div >
    )
  }
}
