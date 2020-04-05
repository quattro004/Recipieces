import React, { Component } from 'react'
import Dropzone from 'react-dropzone'

export class MyDropZone extends Component {
    constructor(props) {
        super(props);
        this.state = {
            droppedFiles: [],
        };
    }

    captureFiles(files) {
        const { droppedFiles } = this.state;
        if (droppedFiles.length === 0) {
            this.setState({
                droppedFiles: files
            });
            return;
        }
        let mergedFiles = files.reduce((accumulator, currentValue) => {
            let filteredFiles = accumulator.filter(file => file.path === currentValue.path);
            if (filteredFiles.length === 0) {
                accumulator.push(currentValue);
            }
            return accumulator;
        }, droppedFiles);
        this.setState({
            droppedFiles: mergedFiles
        });
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

    displayFiles(droppedFiles) {
        return (
            <div className="row row-cols-1 row-cols-md-4">
                {droppedFiles.map(file =>
                    <div className="col-3 mb-4">
                        <div className="card shadow-sm text-white bg-dark">
                            <img alt={file.path}
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
        const { droppedFiles } = this.state;
        let contents = droppedFiles ? this.displayFiles(droppedFiles) : null;

        return (
            <div>
                <Dropzone data-testid="myDropZone"
                    onDropAccepted={droppedFiles => this.captureFiles(droppedFiles)}>
                    {({ getRootProps, getInputProps }) => (
                        <section>
                            <div {...getRootProps()}>
                                <input {...getInputProps({ accept: 'image/*,audio/*,video/*' })} />
                                <div className="card text-white bg-primary col-4">
                                    <h6 className="card-header">Dragon drop or click to select album contents</h6>
                                    <div className="card-body">
                                        <img alt="drop files" src="/drop-zone.png" className="card-img-bottom" />
                                    </div>
                                </div>
                            </div>
                        </section>
                    )}
                </Dropzone>
                <hr />
                <h5>Contents</h5>
                <div>{contents}</div>
            </div>
        )
    }
}

export default MyDropZone