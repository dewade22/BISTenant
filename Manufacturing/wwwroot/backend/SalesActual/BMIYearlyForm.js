let param = window.location.search;
let baseurl = window.location.origin;

$(document).ready(function () {
    Sales();
})

function Sales() {
    $('#tableSales').DataTable({
        stateSave: true,
        autoWidth: true,
        scrollX: true,
        processing: true,
        serverSide: true,
        paging: true,
        searching: { regex: true },
        ajax: {
            url: baseurl + '/BalimoonBMI/SalesActual/YearlySalesDataBMI' + param,
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: function (d) {
                return JSON.stringify(d);
            }
        },
        columns: [
            { data: "documentDate" },
            { data: "soNumber" },
            { data: "documentNo" },
            { data: "salesPerson" },
            { data: "billtoName" },
            { data: "category" },
            { data: "qty" },
            { data: "liters" },
            { data: "cost" },
            { data: "amount" },
            { data: "discount" },
            { data: "tax" },
            { data: "amountIncdTax" },
            { data: "landedCost" },
            { data: "revenue" }
            
        ]
    })
}