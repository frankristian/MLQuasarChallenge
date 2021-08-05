using MLQuasar.Domain.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace MLQuasar.Domain.Tests.Extensions
{
    public class StringListsExtensionsShould
    {
        public class TheMethod_MergeListsShould : StringListsExtensionsShould
        {
            public static TheoryData<List<string>, List<string>, List<string>> Data
            {
                get
                {
                    var data = new TheoryData<List<string>, List<string>, List<string>>();


                    List<string> l1 = new List<string> { "", "", "", "" };
                    List<string> l2 = new List<string> { "", "", "", "" };
                    List<string> exp = new List<string> { "", "", "", "" };
                    data.Add(l1, l2, exp);

                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    l1 = new List<string> { "a", "", "c", "d" };
                    l2 = new List<string> { "a", "", "", "d" };
                    exp = new List<string> { "a", "", "c", "d" };
                    data.Add(l1, l2, exp);

                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    l1 = new List<string> { "a", "", "c", "" };
                    l2 = new List<string> { "", "b", "", "" };
                    exp = new List<string> { "a", "b", "c",""};
                    data.Add(l1, l2, exp);

                                      
                    return data;
                }
            }
            [Theory]
            [MemberData(nameof(Data))]
            public void Return_expected_result(List<string> list1, List<string> list2, List<string> expected)
            {
                //act
                var act = StringListExtentions.MergeLists(list1, list2);
                //assert
                Assert.Equal(expected, act);
            }
            [Fact]
            public void Throws_ArgumentOutOfRangeException()
            {
                //arrange
                List<string> l1 = new List<string> { "", "asdf", "d", "" };
                List<string> l2 = new List<string> { "", "asdf", "d", "","s" };

                //act
                
                //assert
                Assert.Throws<ArgumentOutOfRangeException>(() => StringListExtentions.MergeLists(l1, l2));
               
            }
            [Fact]
            public void Throws_ArgumentException()
            {
                //arrange
                List<string> l1 = new List<string> { "", "asdf", "e", "" };
                List<string> l2 = new List<string> { "", "asdf", "d", "",};

                //act
                
                //assert
                Assert.Throws<ArgumentException>(() => StringListExtentions.MergeLists(l1, l2));

            }

        }
    }
}
