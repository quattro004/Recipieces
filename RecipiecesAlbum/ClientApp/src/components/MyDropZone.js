import React, { Component } from 'react'
import Dropzone from 'react-dropzone'

export class MyDropZone extends Component {
    render() {
        return (
            <div>    
                <Dropzone onDrop={acceptedFiles => console.log(acceptedFiles)}>
                {({getRootProps, getInputProps}) => (
                    <section>
                    <div {...getRootProps()}>
                        <input {...getInputProps()} />
                        <p>~~~~~~~~~~~~~~~~~~~~~
                        <br/>
                        [ Dragon  drop some files here ]
                        <br/>
                        ~~~~~~~~~~~~~~~~~~~~~</p>
                        <p>or click to select files</p>
                    </div>
                    </section>
                )}
                </Dropzone>
            </div>
        )
    }
}

export default MyDropZone