import React from 'react';

export const AlbumRow = function (props) {
    const album = props.item;
    return <tr key={album.id}>
            <td><img alt="musical note" src="./musical-note.png" /></td>
            <td>{album.name}</td>
            <td>{album.description}</td>
            <td>{new Date(album.createdOn).toDateString()}</td>
    </tr>
}