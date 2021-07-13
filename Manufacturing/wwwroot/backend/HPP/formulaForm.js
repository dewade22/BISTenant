let baseUrl = localStorage.getItem('thisAddress');

$(function () {
    $('#btnNext').hide()
    $('.card-hide').hide()
    $('#tableLookup').bootgrid({
        caseSensitive: false,
        formatters: {
            'commands': function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"Push(\'" + row.ModelId + "\',\'" + row.ModelName + "\')\"><span class=\"zmdi zmdi-check-circle\"></span></button>"
            }
        }
    })
    $('#btnNext').click(function () {
        window.location = baseUrl + '/HPPItem/Mixing?ModelId=' + $('#ModelId').val()
    })

})

function LookUp() {
    $('#modalForm').modal()
    $('.modal-title').html('Add New Packaging Material Rates')
}

function Push(Id, Name) {
    $('#ModelId').val(Id)
    $('#modalForm').modal('toggle')
    $('#tableList tbody').empty()
    CekModelExist(Id)
}

function CekModelExist(Id) {
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPItem/CekModelDetail?Id=' + Id,
        success: function (result) {
            
            if (result.length > 0) {
                $.each(result, function (i, val) {
                    let tabel = 
                        `<tr>
                            <td>${val.id}</td>
                            <td>${val.modelId}</td>
                            <td>${val.description}</td>
                            <td>${val.processSize}</td>
                        </tr>`
                    $('#tableList tbody').append(tabel)
                }) 
                
                $('#tableList').bootgrid({
                    caseSensitive: false,
                    formatters: {
                        'command': function (column, row) {
                            return `<button type="button" class="btn btn-primary" onclick="Edit('${row.Id}', '${row.ModelId}')"><i class="zmdi zmdi-edit"></i></button>`
                        }
                    }
                })
                $('.card-hide').show()
                $('#btnNext').show()
            }
            else {
                $('#btnNext').show()
                $('.card-hide').hide()
            }
        },
        error: function (jqXHR, exception) {
            Swal.fire(
                'Error!',
                'error' + jqXHR.status,
                'error'
            )
        }
    })
    
}

function Edit(Id, ModelId) {
    window.location = baseUrl + '/HPPItem/Mixings?ModelId=' +ModelId+'&Id='+Id
}