let baseUrl = localStorage.getItem('thisAddress');

$(function () {
    $('.js-example-basic-single').select2({
        width: 'resolve',
        theme: 'bootstrap4'
    })
    $('.card-hide').hide()
})

$('#save-header').click(function () {
    $(this).hide()
    $('.card-hide').show()
})