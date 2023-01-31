﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let createform_el = document.querySelector('#createform');
function fail(response) {
    //todo
    console.log(response, 'Model error...');
    createform_el.innerHTML = response.responseText;
}

function removeForm() {
    //$('#createform').html = "";
    createform_el.innerHTML="";
}
function addvalidation() {
    const form = createform_el.querySelector('form');
    $.validator.unobtrusive.parse(form);
}
$('#checkbox').click(function () {
    $('form').submit();
})