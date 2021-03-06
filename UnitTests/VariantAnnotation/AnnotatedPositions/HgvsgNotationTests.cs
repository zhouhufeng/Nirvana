﻿using UnitTests.TestDataStructures;
using VariantAnnotation.AnnotatedPositions;
using VariantAnnotation.Interface.Intervals;
using VariantAnnotation.Interface.Positions;
using VariantAnnotation.Interface.Sequence;
using Xunit;

namespace UnitTests.VariantAnnotation.AnnotatedPositions
{
    public class HgvsgNotationTests
    {
        private static ISequence _simpleSequence = new SimpleSequence("ATCGGTGCTGACGATACCTGACGTAAGTA");
        private IInterval _referenceInterval = new Interval(0, _simpleSequence.Length);
        private const string ReferenceAssertion = "NC_012920.1";

        [Theory]
        [InlineData(5,5,"G","T",VariantType.SNV, "NC_012920.1:g.5G>T")]
        [InlineData(5, 7, "GTG", "", VariantType.deletion, "NC_012920.1:g.5_7delGTG")]
        [InlineData(10, 12, "GAC", "", VariantType.deletion, "NC_012920.1:g.12_14delCGA")]
        [InlineData(16, 15, "", "GATA", VariantType.insertion, "NC_012920.1:g.15_16insGATA")]
        //[InlineData(19,22, "TGAC", "GATA", VariantType.MNV, "NC_012920.1:g.19_22delinsGATA")]
        [InlineData(19,22, "TGAC", "GTCA", VariantType.MNV, "NC_012920.1:g.19_22invTGAC")]
        [InlineData(10, 9, "","GAC", VariantType.insertion, "NC_012920.1:g.12_14dupCGA")]
        public void GetNotation_tests(int start,int end,string referenceAllele,string altAllele, VariantType type, string expectedHgvsg)
        {
            var simpleVariant = new SimpleVariant(null,start,end,referenceAllele,altAllele,type);
            var observedHgvsg = HgvsgNotation.GetNotation(ReferenceAssertion, simpleVariant, _simpleSequence, _referenceInterval);
            Assert.Equal(expectedHgvsg,observedHgvsg);
        }
    }
}