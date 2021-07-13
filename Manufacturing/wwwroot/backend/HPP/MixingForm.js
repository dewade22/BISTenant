let baseurl = localStorage.getItem('thisAddress');


$(function () {

    //Validasi
    $('#formHeader').validate({
        onkeyup: function (element) { $(element).valid() }
    })

    //Save Header
    $('#btnAddHeader').click(function () {
        Swal.fire({
            title: 'Save ?',
            text: 'Save Process Size?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya!',
            cancelButtonText: 'Kembali'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: baseurl + '/HPPHelper/SaveDetailProcessHeader',
                    data: $('#formHeader').serialize(),
                    success: function (hasil) {
                        if (hasil.result == 'sukses') {
                            Swal.fire(
                                'Sukses',
                                'Data Tersimpan',
                                'success'
                            ).then((result) => {
                                window.location = baseurl + `/HPPItem/Mixings?ModelId=${$('#ModelId').val()}&Id=${hasil.id}`
                            })
                        } else {
                            Swal.fire(
                                'Error !',
                                'Error ' + result,
                                'error'
                            )
                        }
                    },
                    error: function (jqXHR) {
                        Swal.fire(
                            'Error !!',
                            'Error ' + jqXHR.status,
                            'error'
                        )
                    }
                })
            }
        })
    })

    //Modal Untuk menambahkan mixing size
    $('#AddMixingSize').click(function () {
        $('#modalHeader').modal()
        $('.modal-title').html(`Add Mixing Size`)
    })
})
    

