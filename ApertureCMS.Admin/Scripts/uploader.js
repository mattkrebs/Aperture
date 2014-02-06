$(function () {
    var manualuploader = $('#fine-uploader').fineUploader({
        request: {
            endpoint: '/Upload/SaveFiles'
        },
        autoUpload: false,
        editFilename: {
            enabled: true
        },
        validation: {
            allowedExtensions: ['jpeg', 'jpg', 'png']
        },
        thumbnails: {
            placeholders: {
                waitingPath: "assets/waiting-generic.png",
                notAvailablePath: "assets/not_available-generic.png"
            }
        },

    });
    $('#triggerUpload').click(function () {
        manualuploader.fineUploader('uploadStoredFiles');
    });
    $('#startUpload').click(function () {
        uploader.uploadStoredFiles();
    });


});
