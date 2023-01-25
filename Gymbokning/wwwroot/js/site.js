// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let createform_el = document.querySelector('#createform');
function fail() {
    //todo
    console.log('Model error...');
}

function RemoveForm() {
    //$('#createform').html = "";
    createform_el.innerHTML="";
}
