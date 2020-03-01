import React, { Component } from 'react'
import { Formik, Form, useField } from 'formik';
import * as Yup from 'yup';

export class AlbumForm extends Component {
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
            setTimeout(() => {
              alert(JSON.stringify(values, null, 2));
              setSubmitting(false);
            }, 400);
          }}
        >
          <Form>
            <div class="form-row">
              <div class="form-group col">
                <RecipiecesInput
                  autofocus
                  name="name"
                  type="text"
                  placeholder="Name"
                />
              </div>
            </div>
            <div class="form-row">
              <div class="form-group col">
                <RecipiecesInput
                  name="description"
                  type="text"
                  placeholder="Description"
                />
              </div>
            </div>
            <div class="form-row">
              <div class="form-group col">
                <button type="submit" class="btn btn-primary">Create Album</button>
              </div>
              <a href="/">Back to List</a>
            </div>
          </Form>
        </Formik>
      </div>
    )
  }
}

export default AlbumForm