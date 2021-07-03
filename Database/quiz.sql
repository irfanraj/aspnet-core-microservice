
DO
$$
BEGIN

        CREATE SEQUENCE "public"."quiz_seq_id";

        CREATE TABLE "public"."quiz"
        (
            "id" SMALLINT NOT NULL DEFAULT NEXTVAL('public.quiz_seq_id'),
			"question" TEXT NOT NULL,
			"reward_point" SMALLINT NOT NULL,
			"expire_date" TIMESTAMP NOT NULL ,
			"create_date_time" TIMESTAMP NOT NULL DEFAULT TIMEZONE('UTC',NOW()),
            CONSTRAINT "quiz_pk_id" PRIMARY KEY ("id")
        );

        ALTER SEQUENCE "public"."quiz_seq_id" OWNED BY "public"."quiz"."id";
		
END
$$
LANGUAGE plpgsql;

GRANT SELECT, INSERT ON "public"."quiz" TO "MarketVantageWebUser";
GRANT USAGE, SELECT ON "public"."quiz_seq_id" TO "MarketVantageWebUser";

