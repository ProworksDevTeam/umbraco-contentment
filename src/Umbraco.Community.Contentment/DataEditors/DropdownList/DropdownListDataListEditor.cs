﻿/* Copyright © 2019 Lee Kelleher.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Collections.Generic;
using Umbraco.Core.PropertyEditors;

namespace Umbraco.Community.Contentment.DataEditors
{
    public sealed class DropdownListDataListEditor : IDataListEditor
    {
        public string Name => "Dropdown List";

        public string Description => "Select a single value from a dropdown select list.";

        public string Icon => DropdownListDataEditor.DataEditorIcon;

        public IEnumerable<ConfigurationField> Fields => new ConfigurationField[]
        {
            new ConfigurationField
            {
                Key = DropdownListConfigurationEditor.AllowEmpty,
                Name = "Allow empty?",
                Description = "Enable to allow an empty option at the top of the dropdown list.",
                View = "views/propertyeditors/boolean/boolean.html",
                Config = new Dictionary<string, object>
                {
                    { "default", Constants.Values.True }
                }
            },
            new HtmlAttributesConfigurationField(),
        };

        public Dictionary<string, object> DefaultValues => default;

        public Dictionary<string, object> DefaultConfig => default;

        public bool HasMultipleValues(Dictionary<string, object> config) => false;

        public string View => DropdownListDataEditor.DataEditorViewPath;
    }
}
