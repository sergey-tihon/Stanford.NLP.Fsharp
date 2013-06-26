[<AutoOpen>]
module FSharp.NLP.Stanford.Parser.PennTreebankIITags

// http://www.cis.upenn.edu/~treebank/
type PennTreebankIITags =
    | ROOT
    /// <summary>
    /// Adjective Phrase.
    /// <para> Phrasal category headed by an adjective (including comparative and superlative adjectives). </para>
    /// <para> Example: outrageously expensive.</para>
    /// </summary>
    | ADJP 
    /// <summary>
    /// Adverb Phrase.
    /// <para> Phrasal category headed by an adverb (including comparative and superlative adverbs).</para>
    /// <para> Examples: rather timidly, very well indeed, rapidly. </para>
    /// </summary>
    | ADVP 
    /// <summary>
    /// Coordinating conjunction
    /// <para>This category includes 'and', 'but', 'nor', 'or', 'yet' (as in 'Yet it's cheap', 'cheap yet good'), as well as the
    /// mathematical operators 'plus', 'minus', 'less', 'times' (in the sense of "multiplied by") and 'over' (in the sense of
    /// "divided by"), when they are spelled out.</para>
    /// <para>For in the sense of "because" is a coordinating conjunction (CC) rather than a subordinating conjunction (IN).</para>
    /// <para> He asked to be transferred, for/CC he was unhappy.</para>
    /// <para> So in the sense of "so that," on the other hand, is a subordinating conjunction (IN).</para>
    /// </summary>
    | CC 
    /// Cardinal number
    | CD 
    /// <summary>
    /// Conjunction Phrase.
    /// <para> Used to mark certain "multi-word" conjunctions, such as 'as well as', 'instead of'.</para>
    /// </summary>
    | CONJP 
    /// <summary>
    /// Determiner
    /// <para> This category includes the articles a(n), every, no and the, the indefinite determiners another, any and
    /// some, each, either (as in either way), neither (as in neither decision), that, these, this and those, and
    /// instances of all and both when they do not precede a determiner or possessive pronoun (as in all roads or
    /// both times). (Instances of all or both that do precede a determiner or possessive pronoun are tagged as
    /// predeterminers (PDT).) Since any noun phrase can contain at most one determiner, the fact that such can
    /// occur together with a determiner (as in the only such case) means that it should be tagged as an adjective
    /// (JJ), unless it precedes a determiner, as in such a good time, in which case it is a predeterminer (PDT).</para>
    /// </summary>
    | DT 
    /// <summary>
    /// Existential there
    /// <para> Existential there is the unstressed there that triggers inversion of the inflected verb and 
    /// the logical subject of a sentence. </para>
    /// <para> EXAMPLES: There/EX was a party in progress.; There/EX ensued a melee.</para>
    /// </summary>
    | EX 
    /// Fragment.
    | FRAG 
    /// <summary>
    /// Foreign word
    /// <para>Use your judgment as to what is a foreign word. For me, yoga is an NN, while 'bete noire' and 'persona non
    /// grata' should be tagged bete/FW noire/FW and persona/FW non/FW grata/FW, respectively. </para>
    /// </summary>
    | FW 
    /// <summary>
    /// Preposition or subordinating conjunction
    /// <para> We make no explicit distinction between prepositions and subordinating conjunctions. (The distinction is
    /// not lost, however - a preposition is an IN that precedes a noun phrase or a prepositional phrase, and a
    /// subordinate conjunction is an IN that precedes a clause.) </para>
    /// <para> The preposition to has its own special tag TO. </para>
    /// </summary>
    | IN 
    /// <summary>
    /// Interjection. 
    /// <para> Corresponds approximately to the part-of-speech tag UH.</para>
    /// </summary>
    | INTJ 
    /// <summary>
    ///  Adjective
    /// <para>Hyphenated compounds that are used as modifiers are tagged as adjectives (JJ).</para>
    /// <para>EXAMPLES: happy-go-lucky/JJ; one-of-a-kind/JJ; run-of-the-mill/JJ; </para>
    /// <para>Ordinal numbers are tagged as adjectives (JJ), as are compounds of the form n-th X-est, like fourth-largest.</para>
    /// </summary>
    | JJ 
    /// <summary>
    /// Adjective, comparative
    /// <para>Adjectives with the comparative ending -er and a comparative meaning are tagged JJR. More and less
    /// when used as adjectives, as in more or less mail, are also tagged as JJR. More and less can also be tagged
    /// as JJR when they occur by themselves. Adjectives with a comparative meaning but without the comparative ending -er, 
    /// like superior, should simply be tagged as JJ. Adjectives with the ending -er but without a strictly comparative meaning 
    /// ("more X"), like further in further details, should also simply be tagged as JJ.</para>
    /// </summary>
    | JJR 
    /// <summary>
    /// Adjective, superlative
    /// <para> Adjectives with the superlative ending -est (as well as worst) are tagged as JJS. Most and least when used
    /// as adjectives, as in the most or the least mail, are also tagged as JJS. Most and least can also be tagged as
    /// JJS when they occur by themselves. Adjectives with a superlative meaning but without the superlative ending -est, 
    /// like first, last or unsurpassed, should simply be tagged as JJ.</para>
    /// </summary>
    | JJS 
    /// <summary>
    /// List item marker
    /// <para> This category includes letters and numerals when they are used to identify items in a list. </para>
    /// </summary>
    | LS 
    /// <summary>
    /// List marker. 
    /// <para>Includes surrounding punctuation.</para>
    /// </summary>
    | LST 
    /// <summary>
    /// Modal
    /// <para> This category includes all verbs that don't take an -s ending in the third person singular present: 
    /// 'can', 'could,' '(dare)', 'may', 'might', 'must', 'ought', 'shall', 'should', 'will', 'would'. </para>
    /// </summary>
    | MD 
    /// <summary>
    /// Not a Constituent
    /// <para> used to show the scope of certain prenominal modifiers within an NP.</para>
    /// </summary>
    | NAC 
    ///Noun, singular or mass
    | NN 
    /// Noun, plural
    | NNS 
    ///Proper noun, singular
    | NNP 
    ///Proper noun, plural
    | NNPS 
    /// <summary> 
    /// Noun Phrase. 
    /// <para>Phrasal category that includes all constituents that depend on a head noun.</para>
    /// </summary>
    | NP 
    /// Used within certain complex noun phrases to mark the head of the noun phrase. Corresponds very roughly to N-bar level 
    /// but used quite differently.
    | NX 
    /// <summary>
    /// Predeterminer
    /// <para> This category includes the following determiner like elements when they precede an article or possessive pronoun. </para>
    /// <para> EXAMPLES: all/PDT his marbles; nary/PDT a soul;
    ///                     both/PDT the girls; quite/PDT a mess;
    ///                     half/PDT his time; rather/PDT a nuisance;
    ///                     many/PDT a moon; such/PDT a good time;</para>
    /// </summary>
    | PDT 
    /// <summary>
    /// Possessive ending
    /// <para> The possessive ending on nouns ending in 's or ' is split off by the tagging algorithm and 
    /// tagged as if it were a separate word.</para>
    /// <para> EXAMPLES: John/NNP 's/POS idea; the parents/NNS '/POS distress</para>
    /// </summary>
    | POS 
    /// <summary>
    /// Prepositional Phrase.
    /// <para> Phrasal category headed by a preposition. </para>
    /// </summary>
    | PP 
    /// Parenthetical.
    | PRN 
    /// <summary>
    /// Personal pronoun
    /// <para> This category includes the personal pronouns proper, without regard for case distinctions ('I', 'me', 'you', 'he',
    /// 'him', etc.), the reflexive pronouns ending in -self or -selves, and the nominal possessive pronouns 'mine',
    /// 'yours', 'his', 'hers', 'ours' and 'theirs'. The adjectival possessive forms 'my', 'your', 'his', 'her', 'its', 'our' and 
    /// 'their', on the other hand, are tagged PRPS. </para>
    /// </summary>
    | PRP 
    /// <summary>
    /// Possessive pronoun (prolog version PRP-S)
    /// <para> This category includes the adjectival possessive forms 'my', 'your', 'his', 'her', 'its', 'one's', 'our' and 'their'. 
    /// The nominal possessive pronouns 'mine', 'yours', 'his', 'hers', 'ours' and 'theirs' are tagged as personal pronouns (PRP). </para>
    /// </summary>
    | PRPS
    /// <summary>
    /// Particle. 
    /// <para>Category for words that should be tagged RP. </para>
    /// </summary>
    | PRT 
    ///Quantifier Phrase (i.e. complex measure/amount phrase); used within NP.
    | QP 
    /// <summary>
    /// Adverb
    /// <para>This category includes most words that end in -ly as well as degree words like quite, too and very,
    /// posthead modifiers like enough and indeed (as in good enough, very well indeed), and negative markers like
    /// 'not', 'n't' and 'never'.</para>
    /// </summary>
    | RB 
    /// <summary>
    /// Adverb, comparative
    /// <para> Adverbs with the comparative ending -er but without a strictly comparative meaning, like later in We can
    /// always come by later, should simply be tagged as RB. </para>
    /// </summary>
    | RBR 
    /// Adverb, superlative
    | RBS 
    /// <summary>
    /// Particle
    /// <para> This category includes a number of mostly monosyllabic words that also double as directional adverbs and prepositions.</para>
    /// </summary>
    | RP 
    /// Reduced Relative Clause.
    | RRC
    /// Simple declarative clause, i.e. one that is not introduced by a (possible empty) subordinating conjunction or a wh-word and 
    /// that does not exhibit subject-verb inversion. 
    | S 
    ///Clause introduced by a (possibly empty) subordinating conjunction.
    | SBAR 
    ///Direct question introduced by a wh-word or a wh-phrase. Indirect questions and relative clauses should be bracketed as SBAR, not SBARQ.
    | SBARQ 
    ///Inverted declarative sentence, i.e. one in which the subject follows the tensed verb or modal.
    | SINV 
    ///Inverted yes/no question, or main clause of a wh-question, following the wh-phrase in SBARQ.
    | SQ 
    /// <summary>
    /// Symbol
    /// <para> This tag should be used for mathematical, scientific and technical symbols or expressions that aren't words
    /// of English. It should not used for any and all technical expressions. For instance, the names of chemicals,
    /// units of measurements (including abbreviations thereof) and the like should be tagged as nouns. </para>
    /// </summary>
    | SYM 
    /// <summary>
    /// to
    /// <para>To is tagged TO, regardless of whether it is a preposition or an infinitival marker </para>
    /// </summary>
    | TO 
    /// Unlike Coordinated Phrase
    | UCP 
    /// <summary>
    /// Interjection
    /// <para> This category includes 'my' (as in 'My, what a gorgeous day'), 'oh', 'please', 'see' (as in 'See, it's like this'), 
    /// 'uh', 'well' and 'yes', among others. </para>
    /// </summary>
    | UH 
    /// <summary>
    /// Verb, base form
    /// <para> This tag subsumes imperatives, infinitives and subjunctives.</para>
    /// <para> EXAMPLES: Imperative: Do/VB it.</para>
    /// <para> EXAMPLES: Infinitive: You should do/VB it.; We want them to do/VB it.; We made them do/VB it.; </para>
    /// <para> EXAMPLES: Subjunctive: We suggested that he do/VB it. </para>
    /// </summary>
    | VB 
    /// <summary>
    /// Verb, past tense
    /// <para> This category includes the conditional form of the verb to be. </para>
    /// <para> EXAMPLES: If I were/VBD rich, ... ; If I were/VBD to win the lottery, ... </para>
    /// </summary>
    | VBD 
    /// Verb, gerund or present participle
    | VBG 
    /// Verb, past participle
    | VBN 
    ///Verb, non-3rd person singular present
    | VBP 
    ///Verb, 3rd person singular present
    | VBZ 
    /// <summary>
    /// Verb Phrase. 
    /// <para> Phrasal category headed a verb.</para>
    /// </summary>
    | VP 
    /// <summary> 
    /// Wh-determiner
    /// <para> This category includes which, as well as that when it is used as a relative pronoun. </para>
    /// </summary>
    | WDT 
    /// Wh-adjective Phrase. Adjectival phrase containing a wh-adverb, as in 'how hot'.
    | WHADJP 
    /// Wh-adverb Phrase. Introduces a clause with an NP gap. May be null (containing the 0 complementizer) or lexical, 
    /// containing a wh-adverb such as 'how' or 'why'.
    | WHADVP 
    /// Wh-noun Phrase. Introduces a clause with an NP gap. May be null (containing the 0 complementizer) or lexical, 
    /// containing some wh-word, e.g. 'who', 'which book', 'whose daughter', 'none of which', or 'how many leopards'.
    | WHNP 
    /// Wh-prepositional Phrase. Prepositional phrase containing a wh-noun phrase (such as 'of which' or 'by whose authority') 
    /// that either introduces a PP gap or is contained by a WHNP.
    | WHPP 
    /// <summary>
    /// Wh-pronoun
    /// <para> This category includes 'what', 'who' and 'whom'. </para>
    /// </summary>
    | WP 
    /// <summary>
    /// Possessive wh-pronoun (prolog version WP-S)
    /// <para> This category includes the wh-word 'whose' </para>
    /// </summary>
    | WPS 
    /// <summary>
    /// Wh-adverb
    /// <para> This category includes 'how', 'where', 'why', etc. </para>
    /// <para> When in a temporal sense is tagged WRB. In the sense of "if", on the other hand, it is a subordinating conjunction (IN). </para>
    /// <para> EXAMPLES: When/WRB he finally arrived, I was on my way out.; I like it when/IN you make dinner for me.</para>
    /// </summary>
    | WRB 
    ///Unknown, uncertain, or unbracketable. X is often used for bracketing typos and in bracketing 'the...the'-constructions.
    | X

    /// Punctuation mark, sentence close: '.',';','?','!'
    | SentenceCloser
    /// Punctuation mark, comma: ','.
    | Comma
    /// Punctuation mark, colon: ':'.
    | Colon
    /// Contextual separator, left parenthesis: '(','{','['
    | LRB
    /// Contextual separator, right parenthesis: ')','}',']'
    | RRB
    /// Start quote
    | SQT
    /// End quote
    | EQT

    /// One of special symbol like: '#'
    | Symbol of char