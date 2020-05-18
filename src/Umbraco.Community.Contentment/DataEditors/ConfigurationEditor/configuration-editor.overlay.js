﻿/* Copyright © 2019 Lee Kelleher.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

angular.module("umbraco").controller("Umbraco.Community.Contentment.Overlays.ConfigurationEditor.Controller", [
    "$scope",
    "formHelper",
    function ($scope, formHelper) {

        // console.log("config-editor-overlay.model", $scope.model);

        var defaultConfig = {
            mode: "select",
            autoSelect: true,
            label: "",
            items: [],
            editor: null,
            overlaySize: "large",
            enableFilter: false,
            orderBy: "name"
        };
        var config = Object.assign({}, defaultConfig, $scope.model.config);

        var vm = this;

        function init() {

            vm.mode = config.mode;

            if (vm.mode === "select") {

                if (config.autoSelect && config.items.length === 1) {

                    // NOTE: If there is a single option available, then auto-select it.
                    select(config.items[0]);

                } else {

                    vm.title = "Select " + config.label.toLowerCase() + "...";
                    vm.items = config.items;
                    vm.enableFilter = config.enableFilter;
                    vm.orderBy = config.orderBy;
                    vm.select = select;

                }

            } else if (vm.mode === "edit" && config.editor) {

                var item = $scope.model.value || { key: "", value: {} };
                edit(config.editor, item);

            }

            vm.close = close;
        };

        function edit(editor, item) {

            if ($scope.model.size !== config.overlaySize) {
                $scope.model.size = config.overlaySize;
            }

            vm.title = "Configure " + editor.name;
            vm.editor = angular.copy(editor); // TODO: Replace AngularJS dependency. [LK:2020-03-02]

            if (vm.editor.fields && vm.editor.fields.length > 0) {
                _.each(vm.editor.fields, function (x) { // TODO: Replace Underscore.js dependency. [LK:2020-03-02]
                    x.alias = x.key;
                    x.value = item.value[x.key];
                });
            }

            vm.save = save;
        };

        function select(editor) {
            // If there are no fields, then we can save & close the overlay
            if (_.isEmpty(editor.fields)) { // TODO: Replace Underscore.js dependency. [LK:2020-03-02]
                save(editor);
            } else {
                vm.mode = "edit";
                edit(editor, { value: editor.defaultValues || {} });
            }
        };

        function close() {
            if ($scope.model.close) {
                $scope.model.close();
            }
        };

        function save(item) {

            // NOTE: [LK:2019-06-13] Not sure if we need to use `formHelper.submitForm` here? e.g. `formHelper.submitForm({ scope: $scope, formCtrl: this.configurationEditorForm })`
            // https://github.com/umbraco/Umbraco-CMS/blob/release-8.1.0/src/Umbraco.Web.UI.Client/src/common/services/formhelper.service.js#L26
            $scope.$broadcast("formSubmitting", { scope: $scope });

            var obj = {
                key: item.key,
                value: {}
            };

            item.fields.forEach(function (x) {
                obj.value[x.key] = x.value;
            });

            if ($scope.model.submit) {
                $scope.model.submit(obj);
            }
        };

        init();
    }
]);
