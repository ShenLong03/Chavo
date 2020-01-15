$(document).ready(function () {
    $("#myForm :input").prop("disabled", true);
    $('.textarea-editor').summernote({ height: 120 });
});

/*alertify.defaults.transition = "slide";
alertify.defaults.theme.ok = "btn btn-giroala";
alertify.defaults.theme.cancel = "btn btn-danger";
alertify.defaults.theme.input = "form-control";
$('.onlyDate').datepicker({
    format: "dd/mm/yyyy"
});

var table = $('.listIndex').DataTable({
    "language": {
        "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
        "search": "Buscar",
        "infoEmpty": "Mostrando 0 to 0 of 0 datos    ",
        "infoFiltered": "(filtrar de _MAX_ datos totales)",
        "lengthMenu": "Mostrar _MENU_ datos",
        "loadingRecords": "Cargando...",
        "processing": "Procesando...",
        "zeroRecords": "Ningun registro coincide",
        "paginate": {
            "first": "Primero",
            "last": "Último",
            "next": "Siguiente",
            "previous": "Anterior"
        }
    },
    "dom": ' lfBrtip',
    buttons: [{ extend: 'print', text: '<span class="fa fa-print"></span> Imprimir', className: 'btn btn-sm btn-light' },
    { extend: 'pdfHtml5', text: '<span class="fa fa-file-pdf"></span> PDF', className: 'btn btn-sm btn-light' },
    { extend: 'csvHtml5', text: '<span class="fa fa-file-csv"></span> CSV', className: 'btn btn-sm btn-light' },
    { extend: 'copy', text: '<span class="fa fa-copy"></span> Copiar', className: 'btn btn-sm btn-light' },
    { extend: 'excelHtml5', text: '<span class="fa fa-file-excel"></span> Excel', className: 'btn btn-sm btn-light' }
    ]
});

$('.filter').keyup(function () {
    table.columns(0).search($(this).val().trim());

    table.draw();
});

*/