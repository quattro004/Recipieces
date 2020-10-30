import React, { Component } from 'react'
import Dropzone from 'react-dropzone'
import { Field } from 'formik';

export class MyDropZone extends Component {
    constructor(props) {
        super(props);
        this.MaxSize = 25165824;
        this.state = {
            acceptedFiles: [],
            rejectedFiles: [],
        };
    }

    acceptFiles(files) {
        const { acceptedFiles } = this.state;
        
        if (acceptedFiles.length === 0) {
            this.setState({
                acceptedFiles: files
            });
            return;
        }
        let mergedFiles = files.reduce((accumulator, currentValue) => {
            let filteredFiles = accumulator.filter(file => file.path === currentValue.path);
            if (filteredFiles.length === 0) {
                accumulator.push(currentValue);
            }
            return accumulator;
        }, acceptedFiles);
        this.setState({
            acceptedFiles: mergedFiles
        });
    }

    rejectFiles(rejectedFiles) {
        this.setState({ rejectedFiles: rejectedFiles });
    }

    returnFileSize(number) {
        if (number < 1024) {
            return number + 'bytes';
        } else if (number >= 1024 && number < 1048576) {
            return (number / 1024).toFixed(1) + 'KB';
        } else if (number >= 1048576) {
            return (number / 1048576).toFixed(1) + 'MB';
        }
    }

    displayFiles(acceptedFiles, rejectedFiles) {
        return (
            <div className="row">
                {rejectedFiles.map((file, index, arr) =>
                    <div key={index} 
                        className={rejectedFiles.length === 1 ? "col-12 mb-2" : "col-6 mb-2"}>
                        <div className="card shadow-sm text-danger bg-dark">
                            <div className="card-body">
                                <small className="card-title">
                                    Invalid File: {file.name}
                                </small>
                                <div className="card-title">
                                    Max Size is {this.returnFileSize(this.MaxSize)}
                                </div>
                                <p className="card-text">
                                    <small className="text-muted">
                                       Size: {this.returnFileSize(file.size)} Type: {file.type}
                                    </small>
                                </p>
                            </div>
                        </div>
                    </div>
                )}
                {acceptedFiles.map((file, index, arr) =>
                    <div key={index} className="col-4 mb-2">
                        <div className="card shadow-sm text-white bg-dark">
                            <Field as="img"
                                name={`contents[${index}]`}
                                alt={file.path}
                                src={URL.createObjectURL(file)}
                                className="card-img-top"
                                onLoad={() => { URL.revokeObjectURL(this.src); }} />
                            <div className="card-body">
                                <small className="card-title">{file.name}</small>
                                <p className="card-text">
                                    <small className="text-muted">
                                        Size: {this.returnFileSize(file.size)} Type: {file.type}
                                    </small>
                                </p>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        );
    }

    render() {
        const { acceptedFiles, rejectedFiles } = this.state;
        let contents = (acceptedFiles || rejectedFiles)
            ? this.displayFiles(acceptedFiles, rejectedFiles) 
            : null;

       return (
            <div>
                <Dropzone data-testid="myDropZone" 
                    onDropAccepted={acceptedFiles => this.acceptFiles(acceptedFiles)}
                    onDropRejected={rejectedFiles => this.rejectFiles(rejectedFiles)}
                    minSize={0}
                    maxSize={this.MaxSize}>
                    {({ getRootProps, getInputProps }) => (
                        <section>
                            <div {...getRootProps({ className: 'dropzone' })}>
                                <input {...getInputProps({ accept: 'image/*,audio/*,video/*', multiple: true, name: 'contents' })} />
                                <div className="card text-white bg-primary">
                                    <h6 className="card-header">Dragon drop
                                        <p><i className="text-muted">or click to select album contents</i></p>
                                    </h6>
                                    <div className="card-body col-10">
                                        <img alt="drop files" src="./drop-zone.png" className="card-img-bottom col-3 offset-md-6" />
                                    </div>
                                </div>
                            </div>
                        </section>
                    )}
                </Dropzone>
                <hr />
                <h5>Contents</h5>
                <div className="form-row">{contents}</div>
            </div>
        )
    }
}

export default MyDropZone