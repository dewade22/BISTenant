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
            window.location = baseurl + '/HPPItem/Aging?BoMId=' + $('#BomId').val();

        }
    })
})

let FOHListrik = 0

//Key Press Event
$('#QtyMixer').keyup(function () {
    if ($(this).val() != null && $(this).val() != '' && $('#Batch').val() != null && $('#Batch').val() != '' && $('#TangkiKapasitas').val() != '' && $('#TangkiKapasitas').val() != null) {
        $('#TTotalMixer').html($(this).val() * ($('#TPriceMixer').html() / ($('#Batch').val() % $('#TangkiKapasitas').val() == 0 ? ($('#Batch').val() / $('#TangkiKapasitas').val()) : ($('#Batch').val() / $('#TangkiKapasitas').val())+1 ) / 12))
        calculateTotal()
    }
    $('#TQtyMixer').html($(this).val())
})

$('#TangkiKapasitas').keyup(function () {
    if ($(this).val() != null && $(this).val() != '' && $('#Batch').val() != null && $('#Batch').val() != '' && $('#QtyMixer').val() != null && $('#QtyMixer').val() != '') {
        $('#TTotalMixer').html($('#QtyMixer').val() * ($('#TPriceMixer').html() / ($('#Batch').val() % $(this).val() == 0 ? ($('#Batch').val() / $(this).val()) : ($('#Batch').val() / $(this).val()) + 1) / 12))
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