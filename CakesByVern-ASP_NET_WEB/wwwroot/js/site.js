// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// open sites
function openSite(site) {
    switch (site) {
        case 'location':
            window.open('https://www.google.com/maps/place/jaclupan+campo+2+EEA+VILLAGE+talisay+city+cebu/@10.2851717,123.8203289,15z/data=!4m5!3m4!1s0x0:0xed0856dfa4de6cf3!8m2!3d10.2851717!4d123.8203289');
            break;
        case 'facebook':
            window.open('https://www.facebook.com/CakesbyVern03');
            break;
        case 'youtube':
            window.open('https://www.youtube.com/@thearchibaker5970');
            break;
    }
}

// validate JS
function validate(str) {
    const regex = /^[a-zA-Z0-9._]+\@[a-zA-Z]+\.[a-zA-Z]+$/gm;

    // Alternative syntax using RegExp constructor
    // const regex = new RegExp('^[a-zA-Z0-9._]+\\@[a-zA-Z]+\\.[a-zA-Z]+$', 'gm')
    let m;

    while ((m = regex.exec(str)) !== null) {
        // This is necessary to avoid infinite loops with zero-width matches
        if (m.index === regex.lastIndex) {
            regex.lastIndex++;
        }

        // The result can be accessed through the `m`-variable.
        m.forEach((match, groupIndex) => {
            console.log(`Found match, group ${groupIndex}: ${match}`);
        });
    }
}

