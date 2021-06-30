let baseUrl = localStorage.getItem('thisAddress');

$(function () {
    $('.js-example-basic-single').select2({
        width: 'resolve',
        theme: 'bootstrap'
    })
    $('#tableLookup').bootgrid({
        caseSensitive: false,
        formatters: {
            'commands': function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"Push(\'" + row.ItemNo + "\')\"><span class=\"zmdi zmdi-check-circle\"></span></button>"
            }
        }
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
function LookUpItem() {
    $('#modalForm').modal()
    $('.modal-title').html('Item List')
}
function Push(ItemNo) {
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPHelper/Items?No=' + ItemNo,
        success: function (result) {
            if (Object.keys(result).length > 0) {
                $('#ItemOutputId').val(result.itemNo)
                $('#ItemOutputName').val(result.description)
                $('#QtyOutputUnit').val(result.baseUnitofMeasure)
                $('.js-example-basic-single').trigger('change.select2')
                $('#modalForm').modal('toggle')
                $('#outname').addClass('fg-toggled')
            } else {
                Swal.fire(
                    'Error..!',
                    '404 - Record Not Found',
                    'error'
                )}
            
                
           
        },
        error: function (jqXHR, exception) {
            Swal.fire(
                'ERROR..!',
                'Error ' + jqXHR.status,
                'error'
            )
        }
    })
}

