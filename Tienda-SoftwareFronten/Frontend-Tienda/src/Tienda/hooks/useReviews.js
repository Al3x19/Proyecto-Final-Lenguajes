import { useState} from "react";
import { getBySoftware, createReview, getReviewByUserId, editReview } from "../../shared/actions/Review/review.js";


export const useReview= () => {
    const [Reviews, setReviews] = useState(null);
    const [Review, setReview] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
  
    const loadReviews = async (id, page) => {
      setIsLoading(true);
      const result = await getBySoftware(id, page);
      setReviews(result);
      setIsLoading(false);
    };

  
    const addReview = async (Data) => {
      setIsLoading(true);
      const result = await createReview(Data);
      setReview(result);
      setIsLoading(false);
    };

    const putReview = async (id, Data) => {
      setIsLoading(true);
      const result = await editReview(id, Data);
      setReview(result);
      setIsLoading(false);
    };

    const loadSingleReview = async (id, softwareId) => {
      setIsLoading(true);
      const result = await getReviewByUserId(id, softwareId);
      setReview(result);
      setIsLoading(false);
    };
  
    return {
      Review,
      isLoading,
      Reviews,
      putReview,
      loadReviews,
      addReview,
      loadSingleReview
    };
  };
  