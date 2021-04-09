let baseurl = localStorage.getItem('thisAddress');
$('#btnNext').click(function () {
    Swal.fire({
        title: 'Choose Your Next Process',
        showDenyButton: true,
        showCancelButton: true,
        icon: 'question',
        confirmButtonText: `Next Process`,
        confirmButtonColor: '#03b6fc',
        denyButtonText: `Aging Again`,
        denyButtonColor: '#ffdd00',
    }).then((result) => {
        if (result.isConfirmed) {
            //Next Process
            window.location = baseurl + '/HPPItem/CuciBotol?BoMId=' + $('#BomId').val();
        }
        else if (result.isDenied) {
            //Aging
            window.location = baseurl + '/HPPItem/Aging?BoMId=' + $('#BomId').val();
        }
    })
})