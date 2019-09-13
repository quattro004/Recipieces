// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
'use strict';
const auth = require('solid-auth-client');
const $rdf = require('rdflib')
var Recipieces = Recipieces || {};

$('#login button').click(popupLogin());

Recipieces.auth = (function() {
    async function popupLogin() {
        let session = await auth.currentSession();
        let popupUri = '/recipieces-login-popup.html';
        if (!session) {
            session = await auth.popupLogin({ popupUri });
        }
        alert('Logged in as ${session.webId}');
    }

    // Update components to match the user's login status
    auth.trackSession(session => {
        const loggedIn = !!session;
        // $('#login').toggle(!loggedIn);
        // $('#logout').toggle(loggedIn);
        $('#user').text(session && session.webId);
    });

    return {
        popupLogin: popupLogin
    };
  }());