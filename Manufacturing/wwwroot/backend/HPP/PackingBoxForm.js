let baseurl = localStorage.getItem('thisAddress');
$('#btnNext').click(function () {
    Swal.fire({
        title: 'Next Process',
        text: "You Will Go To The Next Step",
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#03b6fc',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Next'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location = baseurl + '/HPPItem/PackingCukai?BoMId=' + $('#BomId').val();
        }
    })
})