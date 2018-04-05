// Required librairies: Bootbox.js
function BBConfirm(message, href) {	//DIALOGUE DE COMFIRMATION (SUPPRESSION)
    var confirmed = false;
    bootbox.confirm({
        title: ' <div class="BBConfirmBewareImage" style="height:30px;width:30px;"> </div> <b>Attention!<b/>',
        message: message,
        buttons: {
            'cancel': {
                label: 'Annuler',
                className: 'btn-default pull-left'
            },
            'confirm': {
                label: 'Procéder',
                className: 'btn-primary pull-right'
            }
        },
        callback: function (result) {
            confirmed = result;
            if (confirmed) {
                if ((href !== null) && (href !== ''))
                    window.location = href;
            }
        }
    });
    return confirm;
}
// Required librairies: Bootbox.js
function Confirm(message, callBackFunction) {

    bootbox.confirm({
        title: ' <div class="BBConfirmBewareImage" style="height:30px;width:30px;"> </div> <b>Attention!<b/>',
        message: message,
        buttons: {
            'cancel': {
                label: 'Annuler',
                className: 'btn-default pull-left'
            },
            'confirm': {
                label: 'Procéder',
                className: 'btn-primary pull-right'
            }
        },
        callback: function (result) {
            callBackFunction(!result);
        }
    });
}


$(document).ready(function () {
    AppUtilities_BindCallBack();
    $(".data_row .Icon").hide();
});

// Required librairies: jquery-ui.js, jquery.maskedinput.js
function AppUtilities_BindCallBack() {
    $(".phone").mask("(999) 999-9999");		//TOUT LES .PHONE, ONT CE MASK..
    $(".zipcode").mask("a9a 9a9");
    $(".datepicker").attr('type', 'text'); // bypass browser default calendar control
    $(".datepicker").datepicker({
        dateFormat: 'yy-mm-dd',
        changeMonth: true,
        changeYear: true,
        yearRange: "-100:+10",
        dayNamesMin: ["D", "L", "M", "M", "J", "V", "S"],
        monthNamesShort: ["Janv", "Févr", "Mars", "Avr", "Mai", "Juin", "Juil ", "Août", "Sept", "Oct", "Nov", "Déc"]
    });
    $("#ImageUploader").change(function (e) { PreLoadImage(e); });
    $("#UploadButton").click(function () { $("#ImageUploader").trigger("click"); });
    $("#UploadedImage").click(function () { $("#ImageUploader").trigger("click"); });
    ///////////////////////////////////////////////////////////////////////////
    // Hide & Show icons of a dat_row
    $(".data_header").mouseover(function () {
        $(".Icon", this).show();
        $(this).css("background", "#eee");
    });

    $(".data_header").mouseleave(function () {
        $(".Icon", this).hide();
        $(this).css("background", "");
    });

    // for android interface (no mouseover event)
    $(".data_header").click(function () {
        $(".data_row  .Icon").hide();
        $(".data_row").css("background", "");
        $(this).css("background", "#eee");

        $(".Icon", this).show();
    });

    $(".data_row").mouseover(function () {
        $(".Icon", this).show();
        $(this).css("background", "#eee");
    });

    $(".data_row").mouseleave(function () {
        $(".Icon", this).hide();
        $(this).css("background", "");
    });

    // for android interface (no mouseover event)
    $(".data_row").click(function () {
        $(".data_row  .Icon").hide();
        $(".data_row").css("background", "");
        $(this).css("background", "#eee");

        $(".Icon", this).show();
    });

    $(".sorted_header").mouseover(function () {
        $(".Sort_Icon", this).show();
        $(this).css("background", "#ddd");
    });

    $(".sorted_header").mouseleave(function () {
        $(".Sort_Icon", this).hide();
        $(this).css("background", "");
    });

    // for android interface (no mouseover event)
    $(".sorted_header").click(function () {
        $(".sorted_header  .Sort_Icon").hide();
        $(".sorted_header").css("background", "");
        $(this).css("background", "#ddd");
        $(".Sort_Icon", this).show();
    });
}
/*
 * Html sample code for PreLoadImage function usage
 * 
 <label for="UploadedImage" class="control-label col-md-3">Photo</label><br />
        <div class="col-xs-9">
            <img id="UploadedImage"
                    src="@Url.Content(Model.GetPhotoURL())"
                    style="height:86px;max-width:300px;"
                    data-toggle="tooltip"
                    title="Cliquez pour changer de photo..." />
            <input id="ImageUploader"
                    name="ImageUploader"
                    type="file"
                    style="display:none"
                    accept="image/jpeg,image/gif,image/png,image/bmp" /> 
        </div>
*/
function PreLoadImage(e) {
    var imageTarget = $("#UploadedImage")[0];
    var input = $("#ImageUploader")[0];

    if (imageTarget !== null) {
        var len = input.value.length;

        if (len !== 0) {
            var fname = input.value;
            var ext = fname.substr(len - 3, len).toLowerCase();

            if ((ext !== "png") &&
                (ext !== "jpg") &&
                (ext !== "bmp") &&
                (ext !== "gif")) {
                bootbox.alert("Ce n'est pas un fichier d'image de format reconnu. Sélectionnez un autre type de fichier.");
            }
            else {
                var fReader = new FileReader();
                fReader.readAsDataURL(input.files[0]);
                fReader.onloadend = function (event) {
                    // event.target.result contains image data
                    imageTarget.src = event.target.result;
                };
            }
        }
        else {
            imageTarget.src = null;
        }
    }
    return true;
}

