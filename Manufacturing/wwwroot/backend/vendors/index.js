$(function () {
    $('#tableVendors').bootgrid({
        caseSensitive: false,
        css: {
            icon: 'zmdi icon',
            iconColumns: 'zmdi-view-module',
            iconDown: 'zmdi-expand-more',
            iconRefresh: 'zmdi-refresh',
            iconUp: 'zmdi-expand-less'
        },
        formatters: {
            "action": function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" data-row-id=\"" + row.VendorId + "\" onclick=\"Show("+row.VendorId+")\"><span class=\"zmdi zmdi-edit\"></span></button>";
            }
        }
    });

});
function Show(Id) {
    window.location = localStorage.getItem('thisAddress') + '/Vendors/Detil/' + Id;
}