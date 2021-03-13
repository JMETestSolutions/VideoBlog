using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.TestStand.Interop.SequenceAnalyzer;
using NationalInstruments.TestStand.Interop.API;

namespace JMETestSolutions.SequenceAnalyzerRules
{
    /// <summary>
    /// This Class will retrieve all of the requirements in the sequence being analized
    /// </summary>
    public class RequirementsRule
    {
        //-------------------------------------------------------------------------------
        // Constants used by this analysis module. These strings must match those
        // in the Edit Analysis Module dialog box.
        //-------------------------------------------------------------------------------
        private const string cRuleId_getAllRequirements = "JMETestSolutions_getAllRequirements";
        private const string cRuleId_getUnfinishedRequirements = "JMETestSolutions_getUnfinishedRequirements";
        private const string cRuleId_getMalformedRequirements = "JMETestSolutions_getMalformedRequirements";

        private static PropertyObject getRequirementsObject(AnalysisContext analysisContext)
        {
            PropertyObject requirements = null;

            if (analysisContext.Object.Exists("Requirements.Links", 0))
            {
                requirements = analysisContext.Object.GetPropertyObject("Requirements.Links", 0);
            }
            else if (analysisContext.Object.Exists("TS.Requirements.Links", 0))
            {
                requirements = analysisContext.Object.GetPropertyObject("TS.Requirements.Links", 0);
            }

            return requirements;

        }

        public static void getAllRequirements(AnalysisContext analysisContext)
        {
            //Get the rule settings
            RuleConfiguration ruleConfiguration = analysisContext.GetRuleConfiguration(cRuleId_getAllRequirements);
   
            //Only run if the rule is enabled
            if(ruleConfiguration.Enabled)
            {
                //Get the configuration data for this rule
                PropertyObject ruleData = analysisContext.GetRuleAnalysisData(cRuleId_getAllRequirements, RuleAnalysisDataScope.RuleAnalysisDataScope_File, GetRuleAnalysisDataOptions.GetRuleAnalysisDataOption_NoOptions);


                //Only run the analysis if the object we are analyzing contains a Requirements field

                PropertyObject requirements = getRequirementsObject(analysisContext);

                if (requirements != null)
                {                    
                    
                    if(requirements.GetNumElements() > 0) //Only do something if the object has requirements entered
                    {
                        int numberofRequirements = requirements.GetNumElements();
                        string currentRequirement;

                        for (int idx = 0; idx < numberofRequirements; idx++)
                        {

                            currentRequirement = requirements.GetValStringByOffset(idx, 0);

                            string[] requirementparts = currentRequirement.Split(',');

                            if( requirementparts.Length != 3)
                            {
                                //Do nothing. These case should be handled as an error in the getMalforedRequirements Rule.

                            }
                            else
                            {                               
                                    AnalysisMessage message = analysisContext.NewMessage(cRuleId_getAllRequirements, "Requirement " + currentRequirement, analysisContext.Object);
                                    analysisContext.ReportMessage(message);
                            }

                        }

                    }

                }
            }
        }

        public static void getUnfinishedRequirements(AnalysisContext analysisContext)
        {
            //Get the rule settings
            RuleConfiguration ruleConfiguration = analysisContext.GetRuleConfiguration(cRuleId_getUnfinishedRequirements);

            //Only run if the rule is enabled
            if (ruleConfiguration.Enabled)
            {
                //Get the configuration data for this rule
                PropertyObject ruleData = analysisContext.GetRuleAnalysisData(cRuleId_getUnfinishedRequirements, RuleAnalysisDataScope.RuleAnalysisDataScope_File, GetRuleAnalysisDataOptions.GetRuleAnalysisDataOption_NoOptions);

                //Only run the analysis if the object we are analyzing contains a Requirements field

                PropertyObject requirements = getRequirementsObject(analysisContext);                               

                if (requirements != null)
                {                     

                    if (requirements.GetNumElements() > 0) //Only do something if the object has requirements entered
                    {
                        int numberofRequirements = requirements.GetNumElements();
                        string currentRequirement;

                        for (int idx = 0; idx < numberofRequirements; idx++)
                        {

                            currentRequirement = requirements.GetValStringByOffset(idx, 0);

                            string[] requirementparts = currentRequirement.Split(',');

                            if (requirementparts.Length != 3)
                            {
                                //Malformed Requirement Do nothing this should be handled as an error in the getMalformedRequirement Rule
                            }
                            else
                            {                               
                                    if (requirementparts[2].Trim(' ') == "Done")
                                    {
                                        //Do nothing we only want the outstanding requirements

                                    }
                                    else
                                    {
                                        AnalysisMessage message = analysisContext.NewMessage(cRuleId_getUnfinishedRequirements, "Unfinished Requirement " + currentRequirement, analysisContext.Object);
                                        analysisContext.ReportMessage(message);
                                    }
                            }

                        }

                    }

                }
            }

        }

        public static void getMalformedRequirements(AnalysisContext analysisContext)
        {
            //Get the rule settings
            RuleConfiguration ruleConfiguration = analysisContext.GetRuleConfiguration(cRuleId_getMalformedRequirements);

            //Only run if the rule is enabled
            if (ruleConfiguration.Enabled)
            {
                //Get the configuration data for this rule
                PropertyObject ruleData = analysisContext.GetRuleAnalysisData(cRuleId_getMalformedRequirements, RuleAnalysisDataScope.RuleAnalysisDataScope_File, GetRuleAnalysisDataOptions.GetRuleAnalysisDataOption_NoOptions);


                //Only run the analysis if the object we are analyzing contains a Requirements field
                PropertyObject requirements = getRequirementsObject(analysisContext);

                if (requirements != null)
                {                    

                    if (requirements.GetNumElements() > 0) //Only do something if the object has requirements entered
                    {
                        int numberofRequirements = requirements.GetNumElements();
                        string currentRequirement;

                        for (int idx = 0; idx < numberofRequirements; idx++)
                        {

                            currentRequirement = requirements.GetValStringByOffset(idx, 0);

                            string[] requirementparts = currentRequirement.Split(',');

                            if (requirementparts.Length != 3)
                            {

                                AnalysisMessage message = analysisContext.NewMessage(cRuleId_getMalformedRequirements, "Requirement [" + currentRequirement + "] is not formated in Rule,Summary,Status format", analysisContext.Object);
                                analysisContext.ReportMessage(message);
                            }
                        }
                    }

                }
            }

        }
        
    }
}
