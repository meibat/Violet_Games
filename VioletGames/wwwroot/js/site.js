// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function openMenu() {
    document.getElementById('btnMenu').className = 'invisible';
    document.getElementById('tittleMenu').style.marginLeft = '-10%';
    document.getElementById('Content').style.width = '83.5%';
    document.getElementById('Menu').style.width = '16.5%';
    document.getElementById('Top').style.marginLeft = '16.5%';
    document.getElementById('Content').style.marginLeft = '16.5%';
}

function closeMenu() {
    document.getElementById('btnMenu').className = 'navbar-toggler';
    document.getElementById('tittleMenu').style.marginLeft = '0px';
    document.getElementById('Content').style.width = '100%';
    document.getElementById('Menu').style.width = '0px';
    document.getElementById('Top').style.marginLeft = '0px';
    document.getElementById('Content').style.marginLeft = '0px';
}

$(document).ready(function () {
    getDatatable('#table-funcionarios');
    getDatatable('#table-clientes');
    getDatatable('#table-produtos');
    getDatatable('#table-usuarios');
    getDatatable('#table-consoles');
    getDatatable('#table-jogos');
    getDatatable('#table-agendamentos');
});

function getDatatable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "responsive":true,
        "oLanguage": {
            "sEmptyTable": "Ops, nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Ops, nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}