$(document).ready(function () {

    //Evento de click button (Abrir Modal Delete_Album)
    $(".btnOpenModalAlbum").click(function (e) {
        var albumId = $(this).data("album-id");
        OpenModal("DeleteAlbumModal", albumId, '/Home/ShowDeleteAlbum');
    });

    //Evento de click button (Abrir Modal Insert_Music)
    $(".btnAddMusic").click(function (e) {
        var albumId = $("#IdAlbum").val();
        OpenModal("AddMusicModal", albumId, '/Home/ShowInsertMusic');
    });

    //Evento de click button (Abrir Modal Update_Music)
    $(".btnEditMusic").click(function (e) {
        var albumId = $(this).data("id-album");
        var musicId = $(this).data("id-music");
        var indicatorFlag = $(this).data("indicator-flag");

        OpenModalMusic("UpdateMusicModal", musicId, albumId, indicatorFlag, '/Home/ShowUpdateMusic');
    });

    //Evento de click button (Abrir Modal Delete_Music)
    $(".btnDeleteMusic").click(function (e) {
        var albumId = $(this).data("id-album");
        var musicId = $(this).data("id-music");
        var indicatorFlag = $(this).data("indicator-flag");

        OpenModalMusic("DeleteMusicModal", musicId, albumId, indicatorFlag, '/Home/ShowDeleteMusic');
    });

    //Evento de pesquisa a cada caracter digiado
    $("#inputSearch").keyup(function () {
        var value = $(this).val();
        
        $.ajax(
    {
        type: 'GET',
        url: '/Home/IndexSearch',
        dataType: 'html',
        cache: false,
        data: { filter:value },
        async: true,
        success: function (divTarget) {        
            $(".containerHome").html(divTarget);
        }
    });
    });


    //Alterar preview ao adicionar imagem
    $("#file").change(function () {
        const file = $(this)[0].files[0];
        var reader = new FileReader();

        reader.readAsDataURL(file);
        console.log(reader);

        reader.onloadend = function () {
            $("#imagePreview").attr("src", reader.result);
        };
    });

    $
});


//Abre modal para o updateMusic e deleteMusic
function OpenModalMusic(modal, IdMusic, idAlbum, indicatorFlag, url) {
    $.ajax(
{
    type: 'GET',
    url: url,
    dataType: 'html',
    cache: false,
    data: { idMusic: IdMusic, idAlbum: idAlbum, indicatorFlag: indicatorFlag },
    async: true,
    success: function (divTarget) {
        $('.' + modal).html(divTarget);
        $('.' + modal).css("display", "flex");
    }
});
}

//Abre modal genérico
function OpenModal(modal, Id, url) {
    $.ajax(
{
    type: 'GET',
    url: url,
    dataType: 'html',
    cache: false,
    data: { Id: Id },
    async: true,
    success: function (divTarget) {
        $('.' + modal).html(divTarget);
        $('.' + modal).css("display", "flex");
    }
});
}

//Fecha modal
function CloseModal(modal) {
    $('.' + modal).css("display", "none");
}


