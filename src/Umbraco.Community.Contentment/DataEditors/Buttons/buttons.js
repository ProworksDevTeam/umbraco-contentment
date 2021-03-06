﻿/* Copyright © 2020 Lee Kelleher.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

angular.module("umbraco").controller("Umbraco.Community.Contentment.DataEditors.Buttons.Controller", [
    "$scope",
    function ($scope) {

        // console.log("buttons.model", $scope.model);

        var defaultConfig = {
            defaultIcon: "icon-science",
            defaultValue: [],
            items: [],
            enableMultiple: 0
        };
        var config = Object.assign({}, defaultConfig, $scope.model.config);

        var vm = this;

        function init() {
            $scope.model.value = $scope.model.value || config.defaultValue;

            if (Array.isArray($scope.model.value) === false) {
                $scope.model.value = [$scope.model.value];
            }

            vm.icon = config.defaultIcon;
            vm.multiple = Object.toBoolean(config.enableMultiple);

            if (vm.multiple === false && $scope.model.value.length > 0) {
                $scope.model.value.splice(1);
            }

            vm.items = angular.copy(config.items); // TODO: Replace AngularJS dependency. [LK:2020-03-02]

            _.each(vm.items, function (item) { // TODO: Replace Underscore.js dependency. [LK:2020-03-02]
                item.selected = _.contains($scope.model.value, item.value); // TODO: Replace Underscore.js dependency. [LK:2020-03-02]
            });

            vm.select = select;
        };

        function select(item) {

            item.selected = item.selected === false;
            $scope.model.value = [];

            _.each(vm.items, function (x) { // TODO: Replace Underscore.js dependency. [LK:2020-03-02]

                if (vm.multiple === false) {
                    x.selected = x.value === item.value;
                }

                if (x.selected) {
                    $scope.model.value.push(x.value);
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
