namespace ApprenticeWebAPI.Models.Entity
{
    /// <summary>
    /// Copy of MPP Core Patch Request object
    /// </summary>
    public class PatchRequest
    {
        /// <summary>
        /// The action of the payment detail parameter change
        /// </summary>
        public PatchOperations Action { get; set; }

        /// <summary>
        /// The name of the parameter being changed
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// The new payment detail parameter value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// If the action is not set then this holds the original action type
        /// </summary>
        public string InvalidAction { get; set; }
    }
}
