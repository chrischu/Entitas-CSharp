﻿using System;
using System.Collections.Generic;
using System.Linq;
using Entitas.CodeGenerator;

namespace Entitas.CodeGenerator {
    public class SystemExtensionsGenerator : ISystemCodeGenerator {

        const string classSuffix = "GeneratedExtension";

        const string classTemplate = @"namespace Entitas {{
    public partial class Pool {{
        public ISystem Create{0}() {{
            return this.CreateSystem<{1}>();
        }}
    }}
}}";

        public CodeGenFile[] Generate(Type[] systems) {
            return systems
                    .Where(type => type.GetConstructor(new Type[0]) != null)
                    .Aggregate(new List<CodeGenFile>(), (files, type) => {
                        files.Add(new CodeGenFile {
                            fileName = type + classSuffix,
                            fileContent = string.Format(classTemplate, type.Name, type)
                        });
                        return files;
                        })
                    .ToArray();
        }
    }
}
