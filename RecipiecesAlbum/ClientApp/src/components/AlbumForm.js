import React, { Component } from 'react'
import { Formik, Form, useField } from 'formik';
import * as Yup from 'yup';
import { MyDropZone } from './MyDropZone';

const axios = require('axios').default;
const API_POST = 'albums';

export class AlbumForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isLoading: true,
      error: null,
      isCancelling: false,
    };

    this.handleCancel = this.handleCancel.bind(this);
  }

  async createAlbum(albumData) {
    console.debug('Creating album');
    try {
      const response = await axios.post(API_POST, albumData);
      this.setState({ isLoading: false });
      alert(JSON.stringify(response.data, null, 2));
      // TODO: redirect back to list? or allow adding content?
    } catch (err) {
      console.error(err);
      this.setState({ isLoading: false, error: err });
    }
  }

  handleCancel(event) {
    console.debug('handleCancel()');
    this.setState({ isCancelling: true });
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
            {meta.touched && meta.error ? (
              <div className="text-danger">{meta.error}</div>
            ) : null}
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
          initialValues={{ name: '', description: '' }}
          validationSchema={Yup.object({
            name: Yup.string()
              .max(50, 'Must be 50 characters or less')
              .required('Required'),
          })}
          onSubmit={(values, { setSubmitting }) => {
            console.debug('onSubmit()');
            this.createAlbum(values);
          }}
          render={({ values, touched, errors, dirty, isSubmitting }) => (
            <Form>
              <div className="form-row form-group">
                <div className="col-3">
                  <RecipiecesInput
                    autoFocus="autofocus"
                    name="name"
                    type="text"
                    placeholder="Name"
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
              <div className="form-row">
                <div className="form-group col">
                  <MyDropZone />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group col">
                  {isSubmitting ? (
                    <button name="createAlbumButton" type="submit" className="btn btn-primary">
                      Creating...
                      <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                    </button>
                  ) :
                    <button name="createAlbumButton" type="submit" className="btn btn-primary">
                      Create Album
                        </button>
                  }
                </div>
                <a href="/" onClick={this.handleCancel}>Back to List</a>
              </div>
            </Form>
    )
  }
        />
      </div >
    )
  }
}
