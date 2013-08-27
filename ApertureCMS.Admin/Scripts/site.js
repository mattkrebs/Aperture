 $(function () {
        $('#dropZone').filedrop({
            url: '/Upload/UploadPhotos',
            paramname: 'files',
            maxfilesize: 20,
            maxfiles: 5,
            dragOver: function () {
                $('#dropZone').css('background', 'blue');
            },
            dragLeave: function () {
                $('#dropZone').css('background', 'gray');
            },
            drop: function () {
                $('#dropZone').css('background', 'gray');
            },
            afterAll: function () {
                $('#dropZone').html('The file(s) have been uploaded successfully!');
            },
            uploadFinished: function (i, file, response, time) {
                $('#uploadResult').append('<li>' + file.name + '</li>');
            },
            error: function (err, file) {
                switch (err) {
                    case 'BrowserNotSupported':
                        alert('browser does not support HTML5 drag and drop')
                        break;
                    case 'TooManyFiles':
                        alert('Too Many Files')
                        // user uploaded more than 'maxfiles'
                        break;
                    case 'FileTooLarge':
                        alert('File is too large')
                        // program encountered a file whose size is greater than 'maxfilesize'
                        // FileTooLarge also has access to the file which was too large
                        // use file.name to reference the filename of the culprit file
                        break;
                    case 'FileTypeNotAllowed':
                        alert('File Type not supported')
                        // The file type is not in the specified list 'allowedfiletypes'
                    default:
                        break;
                }
            },
            allowedfiletypes: ['image/jpeg', 'image/png', 'image/gif']
        });
 });

 $(function () {
     $("#selectable").selectable();
 });