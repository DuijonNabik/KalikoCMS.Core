﻿#region License and copyright notice
/* 
 * Kaliko Content Management System
 * 
 * Copyright (c) Fredrik Schultz
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 * http://www.gnu.org/licenses/lgpl-3.0.html
 */
#endregion

namespace KalikoCMS.WebForms.Framework {
    using System;
    using KalikoCMS.Core;
    using KalikoCMS.Data;

    public abstract class PageTemplate : System.Web.UI.Page {
        private CmsPage _currentPage;
        private DataStore _dataStoreManager;
        private Guid _pageId;

        public CmsPage CurrentPage {
            get {
                return _currentPage ?? (_currentPage = GetCurrentPageOrDefault());
            }
        }

        protected DataStore PageDataStore {
            get {
                return _dataStoreManager ?? (_dataStoreManager = DataStoreManager.GetStore(CurrentPage));
            }
        }

        // TODO: Reimplement
        //public static string Translate(string key) {
        //    return Language.Translate(key);
        //}

        private CmsPage GetCurrentPageOrDefault() {
            _pageId = Utils.GetCurrentPageId();

            return PageFactory.GetPage(_pageId);
        }

        protected override void OnInit(EventArgs e) {
            RewriteOriginalPath();
        }

        private void RewriteOriginalPath() {
            string originalPath = Utils.GetItem("originalpath");

            if (originalPath != null) {
                Context.RewritePath(originalPath);
            }
        }
    }
}
