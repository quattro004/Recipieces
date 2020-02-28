import React, { Component } from 'react'

class AlbumForm extends Component {
  constructor(props) {
    super(props)

    this.state = { 
      name: '',
      description: ''
    };
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(event) {
    alert('A name was submitted: ' + this.state.name);
    alert('A description was submitted: ' + this.state.description);
    event.preventDefault();
  }

  handleChange(event) {
    const target = event.target;
    const value = target.value;
    const name = target.name;
    
    this.setState({
      [name]: value
    });
  }

  render() {
    return (
      <form id="createAlbum" onSubmit={this.handleSubmit}>
        <div class="form-row">
          <div class="form-group col-6">
            <input class="form-control" autofocus placeholder="Name" name="name" type="text"
              value={this.state.name} onChange={this.handleChange} />
          </div>
        </div>
        <div class="form-row">
          <div class="form-group col-6">
            <input class="form-control" autofocus placeholder="Description" name="description"
              type="text" value={this.state.description} onChange={this.handleChange} />
          </div>
        </div>
        <div class="form-row">
          <div class="form-group col">
            <button type="submit" class="btn btn-primary">Create Album</button>
            <a href="Index">Back to List</a>
          </div>
        </div>
      </form>
    )
  }
}
