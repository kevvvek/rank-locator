import { encode } from "punycode";

interface RankSearchResponse {
    isSuccess: boolean,
    message: string,
    ranks: number[]
}

export default class RankApi {
    static async search(engine: string, keyword: string, url: string) {
        var encodedEngine = encodeURI(engine);
        var encodedKeyword = encodeURI(keyword);
        var encodedUrl = encodeURI(url);
        const response = await fetch(`api/rank/search?engine=${encodedEngine}&keyword=${encodedKeyword}&url=${encodedUrl}`, { method: "POST" });

        if (response.ok) {
            const json = await response.json();
            return json as RankSearchResponse;
        } else {
            const json = {
                isSuccess: false,
                message: "Server Error."
            }
            return json as RankSearchResponse;
        }
    }
}