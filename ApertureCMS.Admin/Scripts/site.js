$(document).ready(function () {
    //Get the checkboxes and disable them
    var boxes = $('input[type=checkbox]').attr('disabled', true);

    //Get the preview image and set the onload event handler
    var preview = $('#preview').load(function () {
        setPreview();
        ias.setOptions({
            x1: 0,
            y1: 0,
            x2: $(this).width(),
            y2: $(this).height(),
            show: true
        });
    });

    //Set the 4 coordinates for the cropping
    var setPreview = function (x, y, w, h) {
        $('#X').val(x || 0);
        $('#Y').val(y || 0);
        $('#Width').val(w || preview[0].naturalWidth);
        $('#Height').val(h || preview[0].naturalHeight);
    };

    //Initialize the image area select plugin
    var ias = preview.imgAreaSelect({
        handles: true,
        instance: true,
        parent: 'body',
        onSelectEnd: function (s, e) {
            var scale = preview[0].naturalWidth / preview.width();
            var _f = Math.floor;
            setPreview(_f(scale * e.x1), _f(scale * e.y1), _f(scale * e.width), _f(scale * e.height));
        }
    });

    //Check one of the checkboxes
    var setBox = function (filter) {
        $('button').attr('disabled', false);
        boxes.attr('checked', false)
        .filter(filter).attr({ 'checked': true, 'disabled': false });
    };

    //Initial state of X, Y, Width and Height is 0 0 1 1
    setPreview(0, 0, 1, 1);

    //Fetch Flickr images
    var fetchImages = function (query) {
        $.getJSON('http://www.flickr.com/services/feeds/photos_public.gne?jsoncallback=?', {
            tags: query,
            tagmode: "any",
            format: "json"
        }, function (data) {
            var screen = $('<div />').addClass('waitScreen').click(function () {
                $(this).remove();
            }).appendTo('body');
            var box = $('<div />').addClass('flickrImages').appendTo(screen);
            $.each(data.items, function (i, v) {
                console.log(data.items[i]);
                $('<img />').addClass('flickrImage').attr('src', data.items[i].media.m).click(function () {
                    $('#Flickr').val(this.src).change();
                    screen.remove();
                }).appendTo(box);
            });
        });
    };

    //Flickr
    $('#FlickrQuery').change(function () {
        fetchImages(this.value);
    });

    $('#Flickr').change(function () {
        setBox('#IsFlickr');
        preview.attr('src', this.value);
    });

    //What happens if the URL changes?
    $('#Url').change(function () {
        setBox('#IsUrl');
        preview.attr('src', this.value);
    });

    //What happens if the File changes?
    $('#File').change(function (evt) {
        var f = evt.target.files[0];
        var reader = new FileReader();

        if (!f.type.match('image.*')) {
            alert("The selected file does not appear to be an image.");
            return;
        }

        setBox('#IsFile');
        reader.onload = function (e) { preview.attr('src', e.target.result); };
        reader.readAsDataURL(f);
    });

    //What happens if any checkbox is checked ?!
    boxes.change(function () {
        setBox(this);
        $('#' + this.id.substr(2)).change();
    });

    $('button').attr('disabled', true);
    $('form').submit(function () {
        $('button').attr('disabled', true).text('Please wait ...');
    });
});