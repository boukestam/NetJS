﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NetJS {
    public class Settings {

        public string Root { get; private set; }

        public string TemplateFolder { get; private set; } = "src/";
        public int DebugPort { get; private set; } = 9222;
        public string Startup { get; private set; } = "startup.js";
        public string Config { get; private set; } = "config.json";
        public string Connections { get; private set; } = "connections.json";
        public string Log { get; private set; } = "log.txt";

        public Settings(string root) {
            SetRoot(root);

            foreach (var key in ConfigurationManager.AppSettings.AllKeys) {
                Set(key, ConfigurationManager.AppSettings[key].ToString());
            }
        }

        public void Set(string key, string value) {
            if (key == "JSFiles") SetRoot(value);
            if (key == "JSTemplates") SetTemplateFolder(value);
            if (key == "JSDebugPort") {
                if (int.TryParse(value, out int port)) {
                    DebugPort = port;
                }
            }
            if (key == "JSStartup") Startup = value;
            if (key == "JSConfig") Config = value;
            if (key == "JSConnections") Connections = value;
            if (key == "JSLog") Log = value;
        }

        public void SetRoot(string root) {
            if (!root.EndsWith("/") && !root.EndsWith("\\")) root += "/";
            Root = root;
        }

        public void SetTemplateFolder(string templateFolder) {
            if (!templateFolder.EndsWith("/") && !templateFolder.EndsWith("\\")) templateFolder += "/";
            TemplateFolder = templateFolder;
        }
    }
}