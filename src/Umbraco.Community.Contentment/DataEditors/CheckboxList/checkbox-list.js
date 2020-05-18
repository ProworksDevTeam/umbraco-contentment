﻿/* Copyright © 2019 Lee Kelleher.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

angular.module("umbraco").controller("Umbraco.Community.Contentment.DataEditors.CheckboxList.Controller", [
    "$scope",
    function ($scope) {

        // console.log("checkboxlist.model", $scope.model);

        var defaultConfig = {
            items: [],
            checkAll: 0,
            showDescriptions: 1,
            showIcons: 0,
            defaultValue: []
        };
        var config = Object.assign({}, defaultConfig, $scope.model.config);

        var vm = this;

        function init() {
            $scope.model.value = $scope.model.value || config.defaultValue;

            if (Array.isArray($scope.model.value) === false) {
                $scope.model.value = [$scope.model.value];
            }

            vm.items = angular.copy(config.items); // TODO: Replace AngularJS dependency. [LK:2020-03-02]

            _.each(vm.items, function (item) { // TODO: Replace Underscore.js dependency. [LK:2020-03-02]
                item.checked = _.contains($scope.model.value, item.value); // TODO: Replace Underscore.js dependency. [LK:2020-03-02]
            });

            vm.showDescriptions = Object.toBoolean(config.showDescriptions);
            vm.showIcons = Object.toBoolean(config.showIcons);

            vm.uniqueId = $scope.model.hasOwnProperty("dataTypeKey")
                ? [$scope.model.alias, $scope.model.dataTypeKey.substring(0, 8)].join("-")
                : $scope.model.alias;

            vm.changed = changed;

            vm.toggleAll = Object.toBoolean(config.checkAll);

            if (vm.toggleAll) {
                vm.toggle = toggle;
                vm.toggleChecked = vm.items.every(function (item) {
                    return item.checked;
                });
            }
        };

        function changed(item) {

            vm.toggleChecked = vm.items.every(function (item) {
                return item.checked;
            });

            $scope.model.value = [];

            _.each(vm.items, function (x) { // TODO: Replace Underscore.js dependency. [LK:2020-03-02]
                if (x.checked) {
                    $scope.model.value.push(x.value);
                }
            });

            setDirty();
        };

        function toggle() {
            $scope.model.value = [];

            _.each(vm.items, function (item) { // TODO: Replace Underscore.js dependency. [LK:2020-03-02]

                item.checked = vm.toggleChecked;

                if (item.checked) {
                    $scope.model.value.push(item.value);
                }
            });

            setDirty();
        };

        function setDirty() {
            if ($scope.propertyForm) {
                $scope.propertyForm.$setDirty();
            }
        };

        init();
    }
]);
