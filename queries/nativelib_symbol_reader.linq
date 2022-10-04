<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
  <RuntimeVersion>7.0</RuntimeVersion>
</Query>

async Task Main()
{
    var headerPath = @"C:\git\guitarrapc\nativebuild-lab\fibo\lib";
    var headerFiles = Directory.EnumerateFiles(headerPath, "*.h", new EnumerationOptions { RecurseSubdirectories = true });
    foreach (var headerFile in headerFiles)
    {
        //var content = await File.ReadAllLinesAsync(headerFile);
        var content = DummyData();

        // search
        var prefix = "prefix_";
        var reader = new SymbolReader();
        var externField = reader.Read(DetectionType.ExternField, content, s => prefix + s).Dump();
        var methods = reader.Read(DetectionType.Method, content, s => prefix + s).Dump();
        var typedefs = reader.Read(DetectionType.Typedef, content, s => prefix + s).Dump();

        // replace
        var implPath = @"C:\git\guitarrapc\nativebuild-lab\fibo\lib";
        var impleFiles = Directory.EnumerateFiles(implPath, "*.c", new EnumerationOptions { RecurseSubdirectories = true });
        var writer = new SymbolWriter();
        foreach (var file in impleFiles)
        {
            var text = await File.ReadAllTextAsync(file);
            var values = text;
            values = writer.ReplaceSymbol(values, methods);
            values = writer.ReplaceSymbol(values, typedefs);
            values.Dump();
        }
    }
}

private string[] DummyData()
{
    return """
    extern const foo_info_t foo_info;
      extern int bar_int;
    extern void (*function_pointer_foo)( const char * test, int line, const char * file );
      extern int ( *function_pointer_bar )();
    extern void* (function_pointer_piyo)( );
    // extern const int comment_out_field;
    externextern const foo_info_t do_not_read;
    externextern void* (do_not_read_function)( );

    typedef uint64_t mbedtls_mpi_uint;
      typedef uint64_t piyopiyo;
    // typedef uint64_t should_be_ignopre_typdef;
    
    typedef uint64_t mbedtls_mpi_uint
    
    typedef enum {
        MBEDTLS_SSL_MODE_STREAM = 0,
        MBEDTLS_SSL_MODE_CBC,
        MBEDTLS_SSL_MODE_CBC_ETM,
        MBEDTLS_SSL_MODE_AEAD
    } mbedtls_ssl_mode_t;

        typedef struct
        {
            void *key;
            mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
            mbedtls_pk_rsa_alt_sign_func sign_func;
            mbedtls_pk_rsa_alt_key_len_func key_len_func;
        } mbedtls_rsa_alt_context;

    // typedef enum {
        MBEDTLS_SSL_MODE_STREAM = 0,
        MBEDTLS_SSL_MODE_CBC,
        MBEDTLS_SSL_MODE_CBC_ETM,
        MBEDTLS_SSL_MODE_AEAD
    } should_be_ignopre_typdef;

    void foo();
       int mbedtls_ssl_write_client_hello( mbedtls_ssl_context *ssl );

    FIBOLIB_API void get_sample_data(sample_data_t *output);
      FIBOLIB_API int fibo(int n);

    mbedtls_ssl_mode_t mbedtls_ssl_get_mode_from_transform(
            const mbedtls_ssl_transform *transform );        
        mbedtls_mpi_uint mbedtls_mpi_core_mla( mbedtls_mpi_uint *d, size_t d_len ,
                                           const mbedtls_mpi_uint *s, size_t s_len,
                                           mbedtls_mpi_uint b );        

    struct foo_struct
    {
        unsigned char *p;       /*!< message, including handshake headers   */
        size_t len;             /*!< length of p                            */
        unsigned char type;     /*!< type of the message: handshake or CCS  */
        mbedtls_ssl_flight_item *next;  /*!< next handshake message(s)              */
    };
    
    static inline int mbedtls_ssl_get_psk( const mbedtls_ssl_context *ssl,
        const unsigned char **psk, size_t *psk_len )
    {
        if( ssl->handshake->psk != NULL && ssl->handshake->psk_len > 0 )
        {
            *psk = ssl->handshake->psk;
            *psk_len = ssl->handshake->psk_len;
        }

        else if( ssl->conf->psk != NULL && ssl->conf->psk_len > 0 )
        {
            *psk = ssl->conf->psk;
            *psk_len = ssl->conf->psk_len;
        }

        else
        {
            *psk = NULL;
            *psk_len = 0;
            return( MBEDTLS_ERR_SSL_PRIVATE_KEY_REQUIRED );
        }

        return( 0 );
    }

    // void should_be_ignopre_method();
    // FIBOLIB_API void should_be_ignopre_method(sample_data_t *output);
    // mbedtls_ssl_mode_t should_be_ignopre_method(
            const mbedtls_ssl_transform *transform );

    /*
     * GCM multiplication: c = a times b in GF(2^128)
     * Based on [CLMUL-WP] algorithms 1 (with equation 27) and 5.
     */

    #ifndef FIBOLIB_VISIBILITY
    #  if defined(__GNUC__) && (__GNUC__ >= 4)
    #    define FIBOLIB_VISIBILITY __attribute__ ((visibility ("default")))
    #  else
    #    define FIBOLIB_VISIBILITY
    #  endif
    #endif

    #define MBEDTLS_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h ) \
        MBEDTLS_BYTES_TO_T_UINT_4( a, b, c, d ),                \
        MBEDTLS_BYTES_TO_T_UINT_4( e, f, g, h )

    #define MBEDTLS_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h )   \
        ( (mbedtls_mpi_uint) (a) <<  0 ) |                        \
        ( (mbedtls_mpi_uint) (b) <<  8 ) |                        \
        ( (mbedtls_mpi_uint) (c) << 16 ) |                        \
        ( (mbedtls_mpi_uint) (d) << 24 ) |                        \
        ( (mbedtls_mpi_uint) (e) << 32 ) |                        \
        ( (mbedtls_mpi_uint) (f) << 40 ) |                        \
        ( (mbedtls_mpi_uint) (g) << 48 ) |                        \
        ( (mbedtls_mpi_uint) (h) << 56 )

    #define MULADDC_X8_CORE                         \
        "movd     %%ecx,     %%mm1      \n\t"   \
        "movd     %%ebx,     %%mm0      \n\t"   \
        "movd     (%%edi),   %%mm3      \n\t"   \
        "paddq    %%mm3,     %%mm1      \n\t"   \
        "movd     (%%esi),   %%mm2      \n\t"   \
        "pmuludq  %%mm0,     %%mm2      \n\t"   \
        "movd     4(%%esi),  %%mm4      \n\t"   \
        "pmuludq  %%mm0,     %%mm4      \n\t"   \
        "movd     8(%%esi),  %%mm6      \n\t"   \
        "pmuludq  %%mm0,     %%mm6      \n\t"   \
        "movd     12(%%esi), %%mm7      \n\t"   \
        "pmuludq  %%mm0,     %%mm7      \n\t"   \
        "paddq    %%mm2,     %%mm1      \n\t"   \
        "movd     4(%%edi),  %%mm3      \n\t"   \
        "paddq    %%mm4,     %%mm3      \n\t"   \
        "movd     8(%%edi),  %%mm5      \n\t"   \
        "paddq    %%mm6,     %%mm5      \n\t"   \
        "movd     12(%%edi), %%mm4      \n\t"   \
        "paddq    %%mm4,     %%mm7      \n\t"   \
        "movd     %%mm1,     (%%edi)    \n\t"   \
        "movd     16(%%esi), %%mm2      \n\t"   \
        "pmuludq  %%mm0,     %%mm2      \n\t"   \
        "psrlq    $32,       %%mm1      \n\t"   \
        "movd     20(%%esi), %%mm4      \n\t"   \
        "pmuludq  %%mm0,     %%mm4      \n\t"   \
        "paddq    %%mm3,     %%mm1      \n\t"   \
        "movd     24(%%esi), %%mm6      \n\t"   \
        "pmuludq  %%mm0,     %%mm6      \n\t"   \
        "movd     %%mm1,     4(%%edi)   \n\t"   \
        "psrlq    $32,       %%mm1      \n\t"   \
        "movd     28(%%esi), %%mm3      \n\t"   \
        "pmuludq  %%mm0,     %%mm3      \n\t"   \
        "paddq    %%mm5,     %%mm1      \n\t"   \
        "movd     16(%%edi), %%mm5      \n\t"   \
        "paddq    %%mm5,     %%mm2      \n\t"   \
        "movd     %%mm1,     8(%%edi)   \n\t"   \
        "psrlq    $32,       %%mm1      \n\t"   \
        "paddq    %%mm7,     %%mm1      \n\t"   \
        "movd     20(%%edi), %%mm5      \n\t"   \
        "paddq    %%mm5,     %%mm4      \n\t"   \
        "movd     %%mm1,     12(%%edi)  \n\t"   \
        "psrlq    $32,       %%mm1      \n\t"   \
        "paddq    %%mm2,     %%mm1      \n\t"   \
        "movd     24(%%edi), %%mm5      \n\t"   \
        "paddq    %%mm5,     %%mm6      \n\t"   \
        "movd     %%mm1,     16(%%edi)  \n\t"   \
        "psrlq    $32,       %%mm1      \n\t"   \
        "paddq    %%mm4,     %%mm1      \n\t"   \
        "movd     28(%%edi), %%mm5      \n\t"   \
        "paddq    %%mm5,     %%mm3      \n\t"   \
        "movd     %%mm1,     20(%%edi)  \n\t"   \
        "psrlq    $32,       %%mm1      \n\t"   \
        "paddq    %%mm6,     %%mm1      \n\t"   \
        "movd     %%mm1,     24(%%edi)  \n\t"   \
        "psrlq    $32,       %%mm1      \n\t"   \
        "paddq    %%mm3,     %%mm1      \n\t"   \
        "movd     %%mm1,     28(%%edi)  \n\t"   \
        "addl     $32,       %%edi      \n\t"   \
        "addl     $32,       %%esi      \n\t"   \
        "psrlq    $32,       %%mm1      \n\t"   \
        "movd     %%mm1,     %%ecx      \n\t"


    """.Split(Environment.NewLine);
}

public enum DetectionType
{
    ExternField,
    Method,
    Typedef,
}
public record SymbolInfo(string Line, DetectionType DetectionType, string Delimiter, string Symbol)
{
    public string? RenamedSymbol { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
};

public class SymbolReader
{
    public IReadOnlyList<SymbolInfo?> Read(DetectionType detectionType, string[] content, Func<string, string> RenameExpression, string? file = null)
    {
        var metadata = new Dictionary<string, string>
        {
            { "file", file ?? "" },
        };
        return detectionType switch
        {
            DetectionType.ExternField => ReadExternFieldInfo(content, RenameExpression, metadata),
            DetectionType.Method => ReadMethodInfo(content, RenameExpression, metadata),
            DetectionType.Typedef => ReadTypedefInfo(content, RenameExpression, metadata),
            _ => throw new NotSupportedException(),
        };
    }

    private IReadOnlyList<SymbolInfo?> ReadExternFieldInfo(string[] content, Func<string, string> RenameExpression, IReadOnlyDictionary<string, string> metadata)
    {
        const string delimiter = ";";
        // `extern int foo;`
        // `extern const foo_t foo;`
        var fieldRegex = new Regex($@"^\s*extern\s+(const\s+)?(?<type>\w+)(\*)?\s+(?<name>\w+)\s*{delimiter}\s*$", RegexOptions.Compiled);
        // `extern void (*foo)( p a );`
        // `extern void* (foo)( );`
        var fieldfunctionRegex = new Regex($@"^\s*extern\s+(?<type>\w+)(\*)?\s+\(\s*\*?(?<name>\w+)\s*\).*{delimiter}\s*$", RegexOptions.Compiled);

        var externLines = ExtractExternFieldLines(content);
        var symbols = externLines
            .Select(x => x.TrimStart())
            .Select(x =>
            {
                var matchFunctionPtr = fieldfunctionRegex.Match(x);
                if (matchFunctionPtr.Success)
                {
                    //match.Dump();
                    var line = matchFunctionPtr.Groups[0].Value;
                    var type = matchFunctionPtr.Groups["type"].Value;
                    var name = matchFunctionPtr.Groups["name"].Value;
                    return new SymbolInfo(line, DetectionType.ExternField, delimiter, name)
                    {
                        RenamedSymbol = RenameExpression.Invoke(name),
                        Metadata = new Dictionary<string, string>(metadata)
                        {
                            {"ReturnType", type},
                        },
                    };
                }

                var matchField = fieldRegex.Match(x);
                if (matchField.Success)
                {
                    //match.Dump();
                    var line = matchField.Groups[0].Value;
                    var type = matchField.Groups["type"].Value;
                    var name = matchField.Groups["name"].Value;
                    return new SymbolInfo(line, DetectionType.ExternField, delimiter, name)
                    {
                        RenamedSymbol = RenameExpression.Invoke(name),
                        Metadata = new Dictionary<string, string>(metadata)
                        {
                            {"ReturnType", type},
                        },
                    };
                }
                else
                {
                    return null;
                }
            })
            .Where(x => x != null)
            .OrderByDescending(x => x?.Symbol.Length)
            .ToArray();

        return symbols;
    }

    private IReadOnlyList<SymbolInfo?> ReadMethodInfo(string[] content, Func<string, string> RenameExpression, IReadOnlyDictionary<string, string> metadata)
    {
        const string delimiter = "(";
        // `foo bar(`
        // ` foo bar(  foo`
        var methodRegex = new Regex($@"\b(?<type>\w+?)\s+(?<method>\w+?)\{delimiter}.*$", RegexOptions.Compiled);
        //content.Dump(path);

        var methodLines = ExtractMethodLines(content);
        var symbols = methodLines
            .Select(x => x.TrimStart())
            .Select(x =>
            {
                var match = methodRegex.Match(x);
                if (match.Success)
                {
                    //match.Dump();
                    var line = match.Groups[0].Value;
                    var type = match.Groups["type"].Value;
                    var name = match.Groups["method"].Value;
                    return new SymbolInfo(line, DetectionType.Method, delimiter, name)
                    {
                        RenamedSymbol = RenameExpression.Invoke(name),
                        Metadata = new Dictionary<string, string>(metadata)
                        {
                            {"ReturnType", type},
                        },
                    };
                }
                else
                {
                    return null;
                }
            })
            .Where(x => x != null)
            .OrderByDescending(x => x?.Symbol.Length)
            .ToArray();

        return symbols;
    }

    private IReadOnlyList<SymbolInfo?> ReadTypedefInfo(string[] content, Func<string, string> RenameExpression, IReadOnlyDictionary<string, string> metadata)
    {
        const string delimiter = ";";
        // `typedef uint64_t foo;`
        var typedefSinglelineRegex = new Regex($@"\btypedef\s+(?<type>\w+)\s+(?<name>\w+){delimiter}", RegexOptions.Compiled);
        // `} foo;`
        var typedefMultilineRegex = new Regex($@"\s*}}\s+(?<name>\w+){delimiter}", RegexOptions.Compiled);

        // Get typedef line in single string
        var typedefLines = ExtractTypedefLines(content);
        var symbols = typedefLines
            .Select(x => x.TrimStart())
            .Select(line =>
            {
                var multilineMatch = typedefMultilineRegex.Match(line);
                if (multilineMatch.Success)
                {
                    //multilineMatch.Dump();
                    var name = multilineMatch.Groups["name"].Value;
                    return new SymbolInfo(line, DetectionType.Typedef, delimiter, name)
                    {
                        RenamedSymbol = RenameExpression.Invoke(name),
                        Metadata = new Dictionary<string, string>(metadata)
                        {
                            {"ReturnType", name}, // Alias Type
                        },
                    };
                }
                else
                {
                    var singlelineMatch = typedefSinglelineRegex.Match(line);
                    if (singlelineMatch.Success)
                    {
                        //singlelineMatch.Dump();
                        var name = singlelineMatch.Groups["name"].Value;
                        return new SymbolInfo(line, DetectionType.Typedef, delimiter, name)
                        {
                            RenamedSymbol = RenameExpression.Invoke(name),
                            Metadata = new Dictionary<string, string>(metadata)
                            {
                                {"ReturnType", name}, // Alias Type
                            },
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            })
            .Where(x => x != null)
            .OrderByDescending(x => x?.Symbol.Length)
            .ToArray();

        return symbols;
    }

    private static IReadOnlyList<string> ExtractExternFieldLines(string[] content)
    {
        static bool IsEmptyLine(string str) => string.IsNullOrWhiteSpace(str);
        static bool IsCommentLine(string str) => str.StartsWith("//") || str.StartsWith("/*") || str.StartsWith("*/") || str.StartsWith("*");
        static bool IsPragmaLine(string str) => str.StartsWith("#");

        var lines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i].TrimStart();
            if (IsEmptyLine(line)) continue;
            if (IsCommentLine(line)) continue;
            if (IsPragmaLine(line)) continue;

            lines.Add(line);
        }

        return lines;
    }

    private static IReadOnlyList<string> ExtractMethodLines(string[] content)
    {
        var parenthesisStartRegex = new Regex(@"^\s*{", RegexOptions.Compiled);

        var defineStartRegex = new Regex(@"\w*#\s*define\s+", RegexOptions.Compiled);
        var defineContinueRegex = new Regex(@".*\\$", RegexOptions.Compiled);

        var structStartRegex = new Regex(@"^\s*struct\s*\w+", RegexOptions.Compiled);
        var structEndRegex = new Regex(@"^\s*};\s*$", RegexOptions.Compiled);

        var staticInlineStartRegex = new Regex(@"\s*static\s+inline\s+\w+\s+\w+", RegexOptions.Compiled);
        var staticInlineEndRegex = new Regex(@"^\s*}", RegexOptions.Compiled);

        static bool IsEmptyLine(string str) => string.IsNullOrWhiteSpace(str);
        static bool IsCommentLine(string str) => str.StartsWith("//") || str.StartsWith("/*") || str.StartsWith("*/") || str.StartsWith("*");
        static bool IsPragmaLine(string str) => str.StartsWith("#");

        var methodLines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i].TrimStart();
            if (IsEmptyLine(line)) continue;
            if (IsCommentLine(line)) continue;

            // skip "#define" block
            if (defineStartRegex.IsMatch(line))
            {
                while (++i <= content.Length - 1 && defineContinueRegex.IsMatch(content[i]))
                {
                }
                continue;
            }

            if (IsPragmaLine(line))
            {
                continue;
            }

            // skip "struct" block
            if (structStartRegex.IsMatch(line))
            {
                var complete = false;
                var rest = 0;
                while (++i <= content.Length - 1 && !complete)
                {
                    // find parenthesis pair which close static inline method.
                    var current = content[i];
                    if (parenthesisStartRegex.IsMatch(content[i]))
                    {
                        rest++;
                    }
                    if (structEndRegex.IsMatch(content[i]))
                    {
                        rest--;
                        complete = rest == 0;
                    }
                    continue;
                }
                continue;
            }

            // skip "static inline" block
            if (staticInlineStartRegex.IsMatch(line))
            {
                var complete = false;
                var rest = 0;
                while (++i <= content.Length - 1 && !complete)
                {
                    // find parenthesis pair which close static inline method.
                    var current = content[i];
                    if (parenthesisStartRegex.IsMatch(content[i]))
                    {
                        rest++;
                    }
                    if (staticInlineEndRegex.IsMatch(content[i]))
                    {
                        rest--;
                        complete = rest == 0;
                    }
                    continue;
                }
                continue;
            }

            methodLines.Add(line);
        }

        return methodLines;
    }

    private static IReadOnlyList<string> ExtractTypedefLines(string[] content)
    {
        const string delimiter = ";";
        // `typedef uint64_t foo;`
        // `typedef enum {
        // } foo;`
        var typedefStartRegex = new Regex(@"^\s*typedef.*", RegexOptions.Compiled);
        // `typedef <any>;` or `} foo;`
        var typedefEndRegex = new Regex($@"(\btypedef.*|}}\s+\w+){delimiter}", RegexOptions.Compiled);

        var typedefLines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i];
            var sb = new StringBuilder();
            if (typedefStartRegex.IsMatch(line))
            {
                // add first line
                sb.AppendLine(line);

                // is typedef single line?
                if (!typedefEndRegex.IsMatch(line))
                {
                    // typedef is multiline

                    // add typedef element lines
                    // stop when semi-colon not found, it is invalid typedef.
                    // stop when new typedef line found, it means invalid typedef found.
                    // stop when typedef last line found.
                    var j = i;
                    while (++j <= content.Length - 1 && !typedefStartRegex.IsMatch(content[j]) && !typedefEndRegex.IsMatch(content[j]))
                    {
                        // {
                        // void* key;
                        // mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
                        // mbedtls_pk_rsa_alt_sign_func sign_func;
                        // mbedtls_pk_rsa_alt_key_len_func key_len_func;
                        sb.AppendLine(content[j]);
                    }

                    // add last line
                    if (j <= content.Length - 1 && typedefEndRegex.IsMatch(content[j]))
                    {
                        // } mbedtls_rsa_alt_context;
                        sb.AppendLine(content[j]);
                    }
                    else
                    {
                        // it is invalid typedef, clear it.
                        sb.Clear();
                    }
                }

                if (sb.Length > 0)
                {
                    typedefLines.Add(sb.ToString());
                }
            }
        }

        return typedefLines;
    }
}

public class SymbolWriter
{
    public string ReplaceSymbol(string content, IReadOnlyList<SymbolInfo?> symbols)
    {
        var current = content;
        foreach (var symbol in symbols)
        {
            if (symbol is not null)
            {
                var from = symbol.Symbol + symbol.Delimiter;
                var to = symbol.RenamedSymbol + symbol.Delimiter;
                if (current.Contains(from))
                {
                    current = current.Replace(from, to);
                }
            }
        }
        return current;
    }
}
