﻿/* Copyright © 2019 Lee Kelleher.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.IO;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;

namespace Umbraco.Community.Contentment.DataEditors
{
    [Core.Composing.HideFromTypeFinder]
    public sealed class UmbracoEntityDataListSource : IDataListSource
    {
        internal static Dictionary<string, UmbracoObjectTypes> SupportedEntityTypes = new Dictionary<string, UmbracoObjectTypes>
        {
            { nameof(UmbracoObjectTypes.DataType), UmbracoObjectTypes.DataType },
            { nameof(UmbracoObjectTypes.Document), UmbracoObjectTypes.Document },
            { nameof(UmbracoObjectTypes.DocumentType), UmbracoObjectTypes.DocumentType },
            { nameof(UmbracoObjectTypes.Media), UmbracoObjectTypes.Media },
            { nameof(UmbracoObjectTypes.MediaType), UmbracoObjectTypes.MediaType },
            { nameof(UmbracoObjectTypes.Member), UmbracoObjectTypes.Member },
            { nameof(UmbracoObjectTypes.MemberType), UmbracoObjectTypes.MemberType },
        };

        internal static Dictionary<string, string> EntityTypeIcons = new Dictionary<string, string>
        {
            { nameof(UmbracoObjectTypes.DataType), Core.Constants.Icons.DataType },
            { nameof(UmbracoObjectTypes.DocumentType), Core.Constants.Icons.ContentType },
            { nameof(UmbracoObjectTypes.MediaType), Core.Constants.Icons.MediaType },
            { nameof(UmbracoObjectTypes.Member),  Core.Constants.Icons.Member },
            { nameof(UmbracoObjectTypes.MemberType),  Core.Constants.Icons.MemberType },
        };

        private readonly IEntityService _entityService;

        public UmbracoEntityDataListSource(IEntityService entityService)
        {
            _entityService = entityService;

            Fields = new ConfigurationField[]
            {
                new NotesConfigurationField(@"<div class=""alert alert-warning"">
<p><strong>A note about supported Umbraco entity types.</strong></p>
<p>Umbraco's `EntityService` API has limited support for querying entity types by <abbr title=""Globally Unique Identifier"">GUID</abbr> or <abbr title=""Umbraco Data Identifier"">UDI</abbr>.</p>
<p>Supported entity types are available in the list below.</p>
</div>", true),
                new ConfigurationField
                {
                    Key = "entityType",
                    Name = "Entity type",
                    Description = "Select the Umbraco entity type to use.",
                    View = IOHelper.ResolveUrl(DropdownListDataEditor.DataEditorViewPath),
                    Config = new Dictionary<string, object>()
                    {
                        { DropdownListConfigurationEditor.AllowEmpty, Constants.Values.False },
                        { DropdownListConfigurationEditor.Items, SupportedEntityTypes.Keys.Select(x => new DataListItem { Name = x.SplitPascalCasing(), Value = x }) },
                    }
                }
            };
        }

        public string Name => "Umbraco Entity";

        public string Description => "Select an Umbraco entity type to populate the data source.";

        public string Icon => "icon-science";

        public IEnumerable<ConfigurationField> Fields { get; }

        public Dictionary<string, object> DefaultValues => default;

        public IEnumerable<DataListItem> GetItems(Dictionary<string, object> config)
        {
            var entityType = config.TryGetValueAs("entityType", out string value)
                ? value
                : string.Empty;

            if (SupportedEntityTypes.TryGetValue(entityType, out var objectType))
            {
                var icon = EntityTypeIcons.ContainsKey(entityType) ? EntityTypeIcons[entityType] : this.Icon;

                return _entityService
                    .GetAll(objectType)
                    .OrderBy(x => x.Name)
                    .Select(x => new DataListItem
                    {
                        Icon = icon,
                        Name = x.Name,
                        Value = Udi.Create(Core.Constants.UdiEntityType.FromUmbracoObjectType(objectType), x.Key).ToString(),
                    });
            }

            return Enumerable.Empty<DataListItem>();
        }
    }
}
