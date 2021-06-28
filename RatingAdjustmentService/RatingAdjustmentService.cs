using System;

namespace RatingAdjustment.Services
{
    /** Service calculating a star rating accounting for the number of reviews
     * 
     */
    public class RatingAdjustmentService
    {
        const double MAX_STARS = 5.0;  // Likert scale
        const double Z = 1.96; // 95% confidence interval

        double _q;
        double _percent_positive;

        /** Percentage of positive reviews
         * 
         * In this case, that means X of 5 ==> percent positive
         * 
         * Returns: [0, 1]
         */
        void SetPercentPositive(double stars)
        {
           _percent_positive= stars/MAX_STARS;
        }

        /**
         * Calculate "Q" given the formula in the problem statement
         */
        void SetQ(double number_of_ratings)
        {
         _q = Math.Round(z * Math.Sqrt((Math.Round((_percent_positive * (1 - _percent_positive))+((z * z)/ (4 * number_of_ratings)),6) / number_of_ratings)),6);
        }

        /** Adjusted lower bound
         * 
         * Lower bound of the confidence interval around the star rating.
         * 
         * Returns: a double, up to 5
         */
        public double Adjust(double stars, double number_of_ratings) {
            SetPercentPositive(stars);
            SetQ(number_of_ratings);
            return MAX_STARS * Math.Round((_percent_positive + ((z*z)/(2*number_of_ratings))-_q))/(1+((z*z)/number_of_ratings)),6);            /
           
        }
    }
}
