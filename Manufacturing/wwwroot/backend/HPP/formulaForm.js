let baseurl = localStorage.getItem('thisAddress');

$('#selectMMEA').change(function () {
    if ($('#selectMMEA').val() !== null) {
        $('#sMMEA2').empty();
        $('#sMMEA2').append(`<option value="">Choose BOM</option>`)
        $.ajax({
            type: 'GET',
            url: baseurl + '/HPPItem/selectBOMLine',
            contentType: 'application/json',
            data: {
                productId : $('#selectMMEA').val()
            },
            success: function (result) {
                result.forEach(function (result) {
                    
                    $('#sMMEA2').append(`<option value="${result.value}">${result.text}</option>`);
                })
                $('#sMMEA2').trigger("chosen:updated");
            }
        })
    }
});

$('#btnNext').click(function () {
    let sMMEA2 = $('#sMMEA2 option:selected').html()
    if (sMMEA2.toLowerCase().indexOf("choose") == -1){
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
                window.location = baseurl + '/HPPItem/PraMixing?BoMId=' + $('#sMMEA2').val();

            }
        })
    } else {
        Swal.fire(
            'Null List of BOM',
            'Please Choose the Options',
            'warning'
        )
    }
})