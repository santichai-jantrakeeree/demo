/// <binding BeforeBuild='_copy_node' />
var gulp = require('gulp');
var del = require('del');
var log = require('fancy-log');

var paths = {
    node_js: [
        'node_modules/jquery/dist/jquery.js',
        'node_modules/jquery-validation/dist/jquery.validate.js',
        'node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js',
        'node_modules/datatables.net/js/jquery.dataTables.js',
        'node_modules/bootstrap/dist/js/bootstrap.js',
        'node_modules/popper.js/dist/umd/popper.js',
        'node_modules/datatables.net/js/jquery.dataTables.js',
        'node_modules/datatables.net-bs4/js/dataTables.bootstrap4.js',
        'node_modules/moment/min/moment.min.js'
    ],
    node_css: [
        'node_modules/bootstrap/dist/css/bootstrap.css',
        'node_modules/datatables.net-bs4/css/dataTables.bootstrap4.css',
        'node_modules/font-awesome/css/font-awesome.css'
    ],
    node_font: [
        'node_modules/font-awesome/fonts/*'
    ],
    vendor_js: ['wwwroot/vendor/js'],
    vendor_css: ['wwwroot/vendor/css'],
    vendor_font: ['wwwroot/vendor/fonts']
}

gulp.task('del:js', function () {
    log('del:js');
    return del(paths.vendor_js);
});

gulp.task('del:css', function () {
    log('del:css');
    return del(paths.vendor_css);
});

gulp.task('del:fonts', function () {
    log('del:fonts');
    return del(paths.vendor_font);
});

gulp.task('copy_node:js', function () {
    log('copy_node:js');
    return gulp.src(paths.node_js)
        .pipe(gulp.dest(paths.vendor_js));
});

gulp.task('copy_node:css', function () {
    log('copy_node:css');
    return gulp.src(paths.node_css)
        .pipe(gulp.dest(paths.vendor_css));
});

gulp.task('copy:fonts', function () {
    return gulp.src(paths.node_font)
        .pipe(gulp.dest(paths.vendor_font))
})

gulp.task('_copy_node', gulp.series('del:js', 'del:css', 'copy_node:js', 'copy_node:css', 'del:fonts', 'copy:fonts'));
