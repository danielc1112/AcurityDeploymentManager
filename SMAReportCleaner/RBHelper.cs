using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMAReportCleaner
{
    //This should really be in a new component which inherits from RichTextBox
    public static class RBHelper
    {
        public static int getWidth(RichTextBox LineNumberTextBox)
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = LineNumberTextBox.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)LineNumberTextBox.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)LineNumberTextBox.Font.Size;
            }
            else
            {
                w = 50 + (int)LineNumberTextBox.Font.Size;
            }

            return w;
        }

        public static void rb_MouseDown(object sender, MouseEventArgs e)
        {
            RichTextBox rb = sender as RichTextBox;
            rb.AutoWordSelection = true;
            rb.AutoWordSelection = false;
        }

        public static void HighlightText(RichTextBox rb, bool OnlyChangeSelectedLines = false)
        {
            if (Config.FileType == FileType.Reports)
            {
                int startLine;
                int endLine;
                if(OnlyChangeSelectedLines)
                {
                    startLine = rb.GetLineFromCharIndex(rb.SelectionStart);
                    endLine   = rb.GetLineFromCharIndex(rb.SelectionStart + rb.SelectionLength);
                }
                else
                {
                    startLine = 0;
                    endLine = rb.Lines.Count() - 1;
                }

                for (int lineNo = startLine; lineNo <= endLine; lineNo++)
                {
                    int index;
                    int startIndex = rb.GetFirstCharIndexFromLine(lineNo);
                    string line = rb.Lines[lineNo];
                    if (startIndex == -1)
                        continue;

                    if (line.StartsWith("#"))
                    {                                              
                        //In memory fields in orange. Slightly wrong, assumes the same keyword won't be found more than once on a line, need a 'until' loop.
                        //Is made faster, by starting at the earliest 'h' found, when going to the next keyword
                        //If there is no 'h' in the line
                        int startOfWordToHighlight = 0;
                        int earliestHFound = 0;
                        foreach (string prefix in Config.inMemoryPrefixes)
                        {
                            startOfWordToHighlight = 0;
                            int endOfWord = -1;
                            //string restOfLine = line;
                            string restOfLine = line.Substring(earliestHFound);
                            startOfWordToHighlight += earliestHFound;
                            earliestHFound = restOfLine.Substring(1).IndexOf('h');
                            if (earliestHFound == -1)
                                break;
                            do
                            {
                                index = restOfLine.IndexOf(prefix); //e.g. FUNCTION(hMDz_Status, blah, hMDz_Member), index -> 10
                                if (index != -1)
                                {
                                    //Need to highlight the rest of the word.
                                    restOfLine = restOfLine.Substring(index);
                                    endOfWord = restOfLine.IndexOfAny(new char[] { ' ', '\t', ')', '=', '+', '-', '*', '/', ',' });

                                    startOfWordToHighlight += index;
                                    if (endOfWord == -1)
                                    {
                                        rb.Select(startIndex + startOfWordToHighlight, restOfLine.Length);
                                        rb.SelectionColor = Config.inMemoryColor;
                                        break; //We've finished with this prefix on this line
                                    }
                                    else
                                    {
                                        rb.Select(startIndex + startOfWordToHighlight, endOfWord);
                                        rb.SelectionColor = Config.inMemoryColor;
                                        restOfLine = restOfLine.Substring(endOfWord);
                                        startOfWordToHighlight += endOfWord;
                                    }
                                }
                            } while (index != -1);
                        }

                        //Data Types. There should be a ':' before it
                        foreach (string dataType in Config.dataTypes)
                        {
                            index = line.IndexOf(dataType);
                            if (index != -1)
                            {
                                string before = line.Substring(0, index);
                                if (line.Substring(0, index).TrimEnd().EndsWith(":"))
                                {
                                    rb.Select(startIndex + index, dataType.Length);
                                    rb.SelectionColor = Config.dataTypeColor;
                                }
                            }
                        }

                        //Literals in red. TRUE,true,FALSE,false,"xxxx",'xxxx',23, 31/12/2018                        
                        index = line.IndexOf("TRUE");
                        if (index != -1)
                        {
                            rb.Select(startIndex + index, 4);
                            rb.SelectionColor = Config.literalColor;
                        }
                        index = line.IndexOf("true");
                        if (index != -1)
                        {
                            rb.Select(startIndex + index, 4);
                            rb.SelectionColor = Config.literalColor;
                        }
                        index = line.IndexOf("FALSE");
                        if (index != -1)
                        {
                            rb.Select(startIndex + index, 5);
                            rb.SelectionColor = Config.literalColor;
                        }
                        index = line.IndexOf("false");
                        if (index != -1)
                        {
                            rb.Select(startIndex + index, 5);
                            rb.SelectionColor = Config.literalColor;
                        }
                        //Need to count number of " found to know where the string literal is.
                        int quoteIndex, nextQuoteIndex;
                        string quoteRestOfLine;

                        //Double quotes
                        quoteRestOfLine = line;
                        startOfWordToHighlight = 0;
                        nextQuoteIndex = -1;
                        do
                        {
                            quoteIndex = quoteRestOfLine.IndexOf("\"");
                            
                            if (quoteIndex != -1)
                            {
                                quoteRestOfLine = quoteRestOfLine.Substring(quoteIndex + 1);
                                startOfWordToHighlight += quoteIndex;
                                nextQuoteIndex = quoteRestOfLine.IndexOf("\"");
                                if (nextQuoteIndex != -1)
                                {
                                    rb.Select(startIndex + startOfWordToHighlight, nextQuoteIndex + 2);
                                    rb.SelectionColor = Config.literalColor;
                                    quoteRestOfLine = quoteRestOfLine.Substring(nextQuoteIndex + 1);
                                    startOfWordToHighlight += nextQuoteIndex + 2;
                                }
                                else
                                    break;
                            }
                        } while (quoteIndex != -1);

                        //Single Quotes
                        quoteRestOfLine = line;
                        startOfWordToHighlight = 0;
                        nextQuoteIndex = -1;
                        do
                        {
                            quoteIndex = quoteRestOfLine.IndexOf("'");

                            if (quoteIndex != -1)
                            {
                                quoteRestOfLine = quoteRestOfLine.Substring(quoteIndex + 1);
                                startOfWordToHighlight += quoteIndex;
                                nextQuoteIndex = quoteRestOfLine.IndexOf("'");
                                if (nextQuoteIndex != -1)
                                {
                                    rb.Select(startIndex + startOfWordToHighlight, nextQuoteIndex + 2);
                                    rb.SelectionColor = Config.literalColor;
                                    quoteRestOfLine = quoteRestOfLine.Substring(nextQuoteIndex + 1);
                                    startOfWordToHighlight += nextQuoteIndex + 2;
                                }
                                else
                                    break;
                            }
                        } while (quoteIndex != -1);

                        //Numbers. If there is a letter character before or after the number, then it's part of a variable name.
                        //This isn't quite right yet
                        string numberRestOfLine = line;
                        startOfWordToHighlight = 0;
                        do
                        {
                            index = numberRestOfLine.IndexOfAny("0123456789.".ToCharArray());
                            //Check if there is a letter or _ before the number
                            bool letterIsBeforeNumber = false;
                            if (index > 0)
                            {
                                char letter = numberRestOfLine[index - 1];
                                letterIsBeforeNumber = Char.IsLetter(letter) || (letter == '_');
                            }

                            if (index != -1)
                            {
                                startOfWordToHighlight += (index + 1);
                                //if(!letterIsBeforeNumber)
                                //{
                                rb.Select(startIndex + startOfWordToHighlight - 1, 1);
                                rb.SelectionColor = Config.literalColor;
                                //}
                                numberRestOfLine = numberRestOfLine.Substring(index + 1);
                            }
                        } while (index != -1);

                        char[] allowedCharBeforeFunctionName = new char[] { '#', ' ', '\t', '(', '=', '+', '-', '*', '/', ',' };

                        index = line.IndexOf("SMA_");
                        if (index != -1)
                        {
                            bool notAFunction = false;
                            if (index > 0)
                            {
                                char chrBeforeFunctionPrefix = line[index - 1];
                                if (!allowedCharBeforeFunctionName.Contains(chrBeforeFunctionPrefix))
                                {
                                    notAFunction = true;
                                }
                            }

                            if (!notAFunction)
                            {
                                //Need to highlight the rest of the word until we find (
                                string restOfLine = line.Substring(index);
                                int endOfFunctionName = restOfLine.IndexOf('(');
                                if (endOfFunctionName != -1)
                                {
                                    rb.Select(startIndex + index, endOfFunctionName);
                                    rb.SelectionColor = Config.knownFunctionColor;
                                }
                            }

                        }

                        //Known functions
                        foreach (string knownFunctionPrefix in Config.knownFunctionPrefixes)
                        {
                            index = line.IndexOf(knownFunctionPrefix);
                            if (index != -1)
                            {
                                bool notAFunction = false;
                                if (index > 0)
                                {
                                    char chrBeforeFunctionPrefix = line[index - 1];
                                    if (!allowedCharBeforeFunctionName.Contains(chrBeforeFunctionPrefix))
                                    {
                                        notAFunction = true;
                                    }
                                }

                                if (!notAFunction)
                                {
                                    //Need to highlight the rest of the word until we find (
                                    string restOfLine = line.Substring(index);
                                    int endOfFunctionName = restOfLine.IndexOf('(');
                                    if (endOfFunctionName != -1)
                                    {
                                        rb.Select(startIndex + index, endOfFunctionName);
                                        rb.SelectionColor = Config.acurityFunctionColor;
                                    }
                                }

                            }
                        }

                        //Keywords in blue. Assumes the same keyword won't be found more than once on a line, need a 'do while' loop.                       
                        foreach (string keyword in Config.keywords)
                        {
                            index = line.IndexOf(keyword);
                            if (index != -1)
                            {
                                //There must be whitespace after the keyword
                                string restOfLine = line.Substring(index + keyword.Length);
                                if ((restOfLine.TrimStart().Length == 0) || (restOfLine.Length != restOfLine.TrimStart().Length))
                                {
                                    //There must be whitespace before the keyword as well, or it's #(keyword) e.g. #VAR
                                    string lineBeforeKeyword = line.Substring(0, index);
                                    if (lineBeforeKeyword == "#" || lineBeforeKeyword.Length != lineBeforeKeyword.TrimEnd().Length)
                                    {
                                        rb.Select(startIndex + index, keyword.Length);
                                        rb.SelectionColor = Config.keywordColor;
                                        if (keyword == "INCLUDE" || keyword == "INCLUDEONCE")
                                        {
                                            int afterKeyword = startIndex + index + keyword.Length;
                                            rb.Select(afterKeyword, restOfLine.Length);
                                            rb.SelectionColor = Config.includeFileColor;
                                        }
                                    }
                                }
                            }
                        }

                        //Comments in green, override anything coloured above
                        index = line.IndexOf("NOTE");
                        if (index != -1)
                        {
                            rb.Select(startIndex + index, line.Length - index);
                            rb.SelectionColor = Config.commentColor;
                        }
                    }

                    //Comments in green, override anything coloured above
                    index = line.IndexOf("//");
                    if (index != -1)
                    {
                        rb.Select(startIndex + index, line.Length - index);
                        rb.SelectionColor = Config.commentColor;
                    }
                }
            }
        }

        private static void DiffHighlightLine(RichTextBox rb, DiffPiece diffLine, ref int rBLineNo)
        {
            string line = diffLine.Text;
            int startIndex, pieceTextStart;

            //Console.WriteLine("lineNo:" + lineNo);
            //if (line != null)
            //    Console.WriteLine("newDiffLine.Type:" + diffLine.Type + " newLine:" + line);
            //else
            //    Console.WriteLine("newDiffLine.Type:" + diffLine.Type + " newLine:null");

            switch (diffLine.Type)
            {
                case ChangeType.Unchanged:
                    startIndex = rb.GetFirstCharIndexFromLine(rBLineNo);
                    rb.Select(startIndex, line.Length);
                    rb.SelectionBackColor = Config.UNCHANGED_COLOUR;
                    rBLineNo++;
                    break;
                case ChangeType.Modified:
                    //Individual character highlighting
                    startIndex = rb.GetFirstCharIndexFromLine(rBLineNo);
                    if (startIndex == -1)
                        return;
                    pieceTextStart = startIndex; //Start of line
                    foreach (DiffPiece piece in diffLine.SubPieces)
                    {
                        if (piece.Text != null)
                        {
                            rb.Select(pieceTextStart, piece.Text.Length);
                            switch (piece.Type)
                            {
                                case ChangeType.Deleted:
                                    rb.SelectionBackColor = Config.DELETED_CHAR_COLOUR;
                                    break;
                                case ChangeType.Inserted:
                                    rb.SelectionBackColor = Config.INSERTED_CHAR_COLOUR;
                                    break;
                                case ChangeType.Modified:
                                    rb.SelectionBackColor = Config.MODIFIED_COLOUR;
                                    break;
                                case ChangeType.Imaginary:
                                    rb.SelectionBackColor = Config.IMAGINARY_COLOUR;
                                    break;
                            }
                            pieceTextStart += piece.Text.Length;
                        }
                    }
                    rBLineNo++;
                    break;
                case ChangeType.Deleted:
                    //Line highlighting
                    startIndex = rb.GetFirstCharIndexFromLine(rBLineNo);
                    if (startIndex == -1)
                        return;
                    rb.Select(startIndex, line.Length);
                    rb.SelectionBackColor = Config.DELETED_COLOUR;
                    rBLineNo++;
                    break;
                case ChangeType.Inserted:
                    //Line highlighting
                    startIndex = rb.GetFirstCharIndexFromLine(rBLineNo);
                    if (startIndex == -1)
                        return;
                    rb.Select(startIndex, line.Length);
                    rb.SelectionBackColor = Config.INSERTED_COLOUR;
                    rBLineNo++;
                    break;
                case ChangeType.Imaginary:
                    break;
            }
        }

        public static void DiffHighlighting(SideBySideDiffBuilder diffBuilder, RichTextBox masterRB, RichTextBox prodRB)
        {
            if (Config.FileType != FileType.Templates)
            {
                SideBySideDiffModel diff = diffBuilder.BuildDiffModel(prodRB.Text, masterRB.Text);

                //4 possibilities:
                //1. Both Unchanged
                //2. Both Modified
                //3. New=Inserted, Old=Imaginary    a line that is in master but not in prod
                //4. New=Imaginary Old=Deleted      a line that is in prod but not in master
                //If it's imaginary, the line doesn't exist in the richtextbox (the way it is displayed in this program)

                //Master
                int rBLineNo = 0;
                for (int lineNo = 0; lineNo < diff.NewText.Lines.Count(); lineNo++)
                {
                    DiffPiece diffLine = diff.NewText.Lines[lineNo];
                    DiffHighlightLine(masterRB, diffLine, ref rBLineNo);
                }

                //PROD
                rBLineNo = 0;
                for (int lineNo = 0; lineNo < diff.OldText.Lines.Count(); lineNo++)
                {
                    DiffPiece diffLine = diff.OldText.Lines[lineNo];
                    DiffHighlightLine(prodRB, diffLine, ref rBLineNo);
                }
            }
        }
    }
}
