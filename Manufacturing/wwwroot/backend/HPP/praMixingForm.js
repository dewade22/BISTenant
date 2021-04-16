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
            window.location = baseurl + '/HPPItem/Mixing?BoMId=' + $('#BomId').val();

        }
    })
})

//Keypress Event
$('#QtyKompor').keyup(function () {
    $('#TQTYKompor').html(`${$(this).val()}`)
    if ($(this).val() != '' || $(this).val() != null) {
        $('#TTotalKompor').html(`${$(this).val() * $('#TPriceKompor').html()}`)
    }
    calculateTotal()
})

$('#MixerCapacity').keyup(function () {
    calculateTotal()
})

$('#LabourType').change(function () {
    $('#labourText').html($('#LabourType option:selected').text())
    $('#TPriceLabour').html($('#LabourType option:selected').val())
    if ($('#TQTYLabour').html() != '' || $('#LabourHour').val() != '' || $('#LabourHour').val() != null) {
        $('#TTotalLabour').html(`${$('#LabourHour').val() * $('#TPriceLabour').html() * $('#TQTYLabour').html()}`)
    }  
    calculateTotal()
})

$('#TotalLabour').keyup(function () {
    $('#TQTYLabour').html(`${$(this).val()}`)
    if ($(this).val() != '' || $(this).val() != null || $('#LabourHour').val() != '' || $('#LabourHour').val() != null) {
        $('#TTotalLabour').html(`${$(this).val() * $('#TPriceLabour').html() * $('#LabourHour').val()}`)
    }
    calculateTotal()
})

$('#LabourHour').keyup(function () {
    if ($(this).val() != null || $(this).val() != '') {
        $('#TTotalLabour').html(`${$(this).val() * $('#TPriceLabour').html() * $('#TQTYLabour').html()}`)
    }
    calculateTotal()
})

$('#FOHElectricity').keyup(function () {
    if ($(this).val() != '' || $(this).val() != null) {
        $('#TQTYElectricity').html($(this).val())
        $('#TTotalElectricity').html($(this).val() * $('#TPriceElectricity').html())
        calculateTotal()
    }
})

$('#FOHFuel').keyup(function () {
    if ($(this).val() != '' || $(this).val() != null) {
        $('#TQTYFuel').html($(this).val())
        $('#TTotalFuel').html($(this).val() * $('#TPriceFuel').html())
        calculateTotal()
    }
})

$('#FOHWater').keyup(function () {
    if ($(this).val() != null || $(this).val() != '') {
        $('#TQTYWater').html($(this).val())
        $('#TTotalWater').html($(this).val() * $('#TPriceWater').html())
        calculateTotal()
    }
})

$('#FOHGas').keyup(function () {
    if ($(this).val() != null || $(this).val() != '') {
        $('#TQTYGas').html($(this).val())
        $('#TTotalGas').html($(this).val() * $('#TPriceGas').html())
        calculateTotal()
    }
})

function calculateTotal() {
    let ttl = $('.total');
    let total = 0
    $.each(ttl, function (i, ttl) {
        let pc = $(this).text()
        if (!isNaN(parseFloat($(this).text()))) {
            total = total + parseFloat(pc, 1000)
        }
    });
    $('#TotalPerMachine').val(total)
    if (total != null || total != '') {
        TotalperUOM(total)
    }
    
}

function TotalperUOM(total) {
    if ($('#MixerCapacity').val() != null || $('#MixerCapacity').val() != '') {
        let totalUOM = (total / $('#MixerCapacity').val())
        $('#TotalPerUoM').val(totalUOM)
    }
}