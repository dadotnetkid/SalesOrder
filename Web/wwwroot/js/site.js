// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showLoading() {
    $('.loading').css({
        visibility: 'visible'
    })
}

function hideLoading() {
    $('.loading').css({
        visibility: 'hidden'
    })
}

function onShow() {
    this.show();
}
function showModalDetail(url, data, target, modalId) {
    if ($(modalId).length) {
        $(modalId).remove();
    }
    $.get(url,
        data,
        function (xhr) {
            $(target).html(xhr);
        });
}

function deleteItem(url, data, callback) {
    var c = confirm("Do you want to delete this item");
    if (c === true)
        $.post(url, data, callback);
}