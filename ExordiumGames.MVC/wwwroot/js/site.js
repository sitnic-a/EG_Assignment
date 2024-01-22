﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", (e) => {
    console.log("DOM fully loaded and parsed");
    if (window.history.replaceState) {
        window.history.replaceState(null, null, window.location.href);
    }
});