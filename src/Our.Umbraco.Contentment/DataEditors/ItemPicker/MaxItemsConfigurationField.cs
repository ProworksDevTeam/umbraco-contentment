﻿/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using Umbraco.Core.PropertyEditors;

namespace Our.Umbraco.Contentment.DataEditors
{
    internal class MaxItemsConfigurationField : ConfigurationField
    {
        public const string MaxItems = "maxItems";

        public MaxItemsConfigurationField()
            : base()
        {
            Key = MaxItems;
            Name = "Maximum items";
            Description = "Enter the number for the maximum items allowed.<br>Use '0' for an unlimited amount.";
            View = "number";
        }
    }
}