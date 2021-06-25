let baseUrl = localStorage.getItem('thisAddress');

$(function () {
    $('.js-example-basic-single').select2({
        width: 'resolve',        
    })

    //Validasi
    $('#form-Input').validate({
        onkeyup: function (element) { $(element).valid() }
    })

    $('#save-header').click(function () {
        if ($('#form-Input').valid()) {
            if ($('#QtyOutputUnit').val() == null) {
                $('#error-select').html('Please Select Unit First!!')
            } else {
                $.ajax({
                    type: 'POST',
                    url: baseUrl + '/HPPItem/SaveHeaderPraMixing',
                    data: $('#form-Input').serialize(),
                    success: function (result) {
                        if (result.result == 'sukses') {
                            Swal.fire(
                                'Sukses',
                                'Data Berhasil Ditambahkan',
                                'success',
                            ).then((hasil) => {
                                window.location = 'PraMixings?Header=' + result.modelId
                            })
                        } else {
                            Swal.fire(
                                'Error !!',
                                result.result,
                                'error'
                            )
                        }
                    },
                    error: function (jqXHR, exception) {
                        Swal.fire(
                            'ERROR !!',
                            '' + jqXHR.status,
                            'error'
                        )
                    }
                })
            }
        }
    })

})

