//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.11.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from c:\Users\Wenjie\OneDrive\MasterThesis\VisionBasedCompiler\BioCompiler\Visitors\Syntax.g by ANTLR 4.11.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
[System.CLSCompliant(false)]
public partial class SyntaxLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, ID=13, INT=14, FLOAT=15, COMMENT=16, SPACE=17;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "ID", "INT", "FLOAT", "COMMENT", "SPACE"
	};


	public SyntaxLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public SyntaxLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'droplet'", "';'", "'input('", "','", "')'", "'move'", "'('", "'merge'", 
		"'split'", "'mix'", "'output'", "'store'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, "ID", "INT", "FLOAT", "COMMENT", "SPACE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Syntax.g"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static SyntaxLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,17,121,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,2,1,
		2,1,2,1,2,1,2,1,2,1,2,1,3,1,3,1,4,1,4,1,5,1,5,1,5,1,5,1,5,1,6,1,6,1,7,
		1,7,1,7,1,7,1,7,1,7,1,8,1,8,1,8,1,8,1,8,1,8,1,9,1,9,1,9,1,9,1,10,1,10,
		1,10,1,10,1,10,1,10,1,10,1,11,1,11,1,11,1,11,1,11,1,11,1,12,1,12,5,12,
		95,8,12,10,12,12,12,98,9,12,1,13,4,13,101,8,13,11,13,12,13,102,1,14,1,
		14,1,14,1,14,1,15,1,15,5,15,111,8,15,10,15,12,15,114,9,15,1,15,1,15,1,
		16,1,16,1,16,1,16,0,0,17,1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,
		21,11,23,12,25,13,27,14,29,15,31,16,33,17,1,0,5,3,0,65,90,95,95,97,122,
		4,0,48,57,65,90,95,95,97,122,1,0,48,57,2,0,10,10,13,13,3,0,9,10,13,13,
		32,32,123,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,
		0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,
		1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,
		0,0,33,1,0,0,0,1,35,1,0,0,0,3,43,1,0,0,0,5,45,1,0,0,0,7,52,1,0,0,0,9,54,
		1,0,0,0,11,56,1,0,0,0,13,61,1,0,0,0,15,63,1,0,0,0,17,69,1,0,0,0,19,75,
		1,0,0,0,21,79,1,0,0,0,23,86,1,0,0,0,25,92,1,0,0,0,27,100,1,0,0,0,29,104,
		1,0,0,0,31,108,1,0,0,0,33,117,1,0,0,0,35,36,5,100,0,0,36,37,5,114,0,0,
		37,38,5,111,0,0,38,39,5,112,0,0,39,40,5,108,0,0,40,41,5,101,0,0,41,42,
		5,116,0,0,42,2,1,0,0,0,43,44,5,59,0,0,44,4,1,0,0,0,45,46,5,105,0,0,46,
		47,5,110,0,0,47,48,5,112,0,0,48,49,5,117,0,0,49,50,5,116,0,0,50,51,5,40,
		0,0,51,6,1,0,0,0,52,53,5,44,0,0,53,8,1,0,0,0,54,55,5,41,0,0,55,10,1,0,
		0,0,56,57,5,109,0,0,57,58,5,111,0,0,58,59,5,118,0,0,59,60,5,101,0,0,60,
		12,1,0,0,0,61,62,5,40,0,0,62,14,1,0,0,0,63,64,5,109,0,0,64,65,5,101,0,
		0,65,66,5,114,0,0,66,67,5,103,0,0,67,68,5,101,0,0,68,16,1,0,0,0,69,70,
		5,115,0,0,70,71,5,112,0,0,71,72,5,108,0,0,72,73,5,105,0,0,73,74,5,116,
		0,0,74,18,1,0,0,0,75,76,5,109,0,0,76,77,5,105,0,0,77,78,5,120,0,0,78,20,
		1,0,0,0,79,80,5,111,0,0,80,81,5,117,0,0,81,82,5,116,0,0,82,83,5,112,0,
		0,83,84,5,117,0,0,84,85,5,116,0,0,85,22,1,0,0,0,86,87,5,115,0,0,87,88,
		5,116,0,0,88,89,5,111,0,0,89,90,5,114,0,0,90,91,5,101,0,0,91,24,1,0,0,
		0,92,96,7,0,0,0,93,95,7,1,0,0,94,93,1,0,0,0,95,98,1,0,0,0,96,94,1,0,0,
		0,96,97,1,0,0,0,97,26,1,0,0,0,98,96,1,0,0,0,99,101,7,2,0,0,100,99,1,0,
		0,0,101,102,1,0,0,0,102,100,1,0,0,0,102,103,1,0,0,0,103,28,1,0,0,0,104,
		105,3,27,13,0,105,106,5,46,0,0,106,107,3,27,13,0,107,30,1,0,0,0,108,112,
		5,35,0,0,109,111,8,3,0,0,110,109,1,0,0,0,111,114,1,0,0,0,112,110,1,0,0,
		0,112,113,1,0,0,0,113,115,1,0,0,0,114,112,1,0,0,0,115,116,6,15,0,0,116,
		32,1,0,0,0,117,118,7,4,0,0,118,119,1,0,0,0,119,120,6,16,0,0,120,34,1,0,
		0,0,4,0,96,102,112,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
