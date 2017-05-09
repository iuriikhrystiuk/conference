var path = require("path");

const EVENT = process.env.npm_lifecycle_event || "";
const baseFolder = "Client";

var ROOT = path.resolve(__dirname, "../..");

function hasProcessFlag(flag) {
    return process.argv.join("").indexOf(flag) > -1;
}

function hasNpmFlag(flag) {
    return EVENT.includes(flag);
}

function isWebpackDevServer() {
    return process.argv[1] && !!(/webpack-dev-server/.exec(process.argv[1]));
}

function concatPath(location) {
    return `./${baseFolder}/${location}`;
}

var root = path.join.bind(path, ROOT);

exports.hasProcessFlag = hasProcessFlag;
exports.hasNpmFlag = hasNpmFlag;
exports.isWebpackDevServer = isWebpackDevServer;
exports.root = root;
exports.baseFolder = baseFolder;
exports.concatPath = concatPath;