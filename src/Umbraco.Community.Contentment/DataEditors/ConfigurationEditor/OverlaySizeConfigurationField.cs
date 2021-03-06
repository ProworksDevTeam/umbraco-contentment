﻿/* Copyright © 2019 Lee Kelleher.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Collections.Generic;
using Umbraco.Core.IO;
using Umbraco.Core.PropertyEditors;

namespace Umbraco.Community.Contentment.DataEditors
{
    internal sealed class OverlaySizeConfigurationField : ConfigurationField
    {
        internal const string OverlaySize = "overlaySize";
        internal const string Small = "small";
        internal const string Large = "large";

        public OverlaySizeConfigurationField()
            : this(Small)
        { }

        public OverlaySizeConfigurationField(string defaultSize)
            : base()
        {
            Key = OverlaySize;
            Name = "Editor overlay size";
            Description = "Select the size of the overlay editing panel. By default this is set to 'large'. However if the editor fields require a smaller panel, select 'small'.";
            View = IOHelper.ResolveUrl(RadioButtonListDataEditor.DataEditorViewPath);
            Config = new Dictionary<string, object>
            {
                { RadioButtonListConfigurationEditor.Items, new[]
                    {
                        new DataListItem { Name = nameof(Small), Value = Small },
                        new DataListItem { Name = nameof(Large), Value = Large }
                    }
                },
                { RadioButtonListConfigurationEditor.DefaultValue, defaultSize }
            };
        }
    }
}
