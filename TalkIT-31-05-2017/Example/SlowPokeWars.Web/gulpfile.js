var ts = require("gulp-typescript");
var gulp = require("gulp");
var clean = require("gulp-clean");

var destPath = "./Scripts/libs/npm/";

// Delete the dist directory
gulp.task("clean", function () {
    return gulp.src(destPath)
        .pipe(clean());
});

gulp.task("scriptsNStyles", function () {
    gulp.src([
            "core-js/client/*.js",
            "systemjs/dist/*.js",
            "reflect-metadata/*.js",
            "rxjs/**",
            "zone.js/dist/*.js",
            "@angular/**/bundles/*.js",
            "bootstrap/dist/js/*.js"
        ], {
            cwd: "node_modules/**"
        })
        .pipe(gulp.dest("./Scripts/libs/npm/"));
});

var tsProject = ts.createProject("App/tsconfig.json", {
    typescript: require("typescript")
});
gulp.task("ts", function () {
    var tsResult = gulp.src([
            "App/*.ts"
        ])
        .pipe(tsProject(), undefined, ts.reporter.fullReporter());
    return tsResult.js.pipe(gulp.dest("./Scripts/app"));
});

gulp.task("watch", ["watch.ts"]);

gulp.task("watch.ts", ["ts"], function () {
    return gulp.watch("App/*.ts", ["ts"]);
});

gulp.task("default", ["scriptsNStyles", "watch"]);