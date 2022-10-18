// Karma configuration file, see link for more information
// https://karma-runner.github.io/1.0/config/configuration-file.html

module.exports = function (config) {
  config.set({
    basePath: "",
    frameworks: ["jasmine", "@angular-devkit/build-angular"],
    plugins: [
      require("karma-jasmine"),
      require("karma-chrome-launcher"),
      require("karma-coverage"),
      require("karma-spec-reporter"),
      require("karma-junit-reporter"),
      require("@angular-devkit/build-angular/plugins/karma"),
    ],
    junitReporter: {
      outputDir: "tmp/junit",
    },
    coverageReporter: {
      dir: require("path").join(__dirname, "./tmp"),
      subdir: ".",
      reporters: [{ type: "cobertura", subdir: "cobertura" }],
    },
    junitReporter: {
      outputDir: "tmp/junit",
    },
    coverageReporter: {
      dir: require("path").join(__dirname, "./tmp"),
      subdir: ".",
      reporters: [{ type: "cobertura", subdir: "cobertura" }],
    },
    reporters: ["junit", "spec"],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: true,
    browsers: ["ChromeHeadlessNoSandbox"],
    customLaunchers: {
      ChromeHeadlessNoSandbox: {
        base: "ChromeHeadless",
        flags: ["--no-sandbox"],
      },
    },
    singleRun: true,
  });
};
