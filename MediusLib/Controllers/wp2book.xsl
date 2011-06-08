<xsl:transform version="2.0"
               xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
               xmlns:dc="http://purl.org/dc/elements/1.1/"
               xmlns:content="http://purl.org/rss/1.0/modules/content/"
               xmlns:wp="http://wordpress.org/export/1.1/"
               xmlns:months="f:months"
			   xmlns:medius="http://philiphanson.org/medius/book/1.0">
<xsl:output method="xml" indent="yes" cdata-section-elements="post" />

<!-- use these parameters to supply values for the book/chapter metadata. they are included so the output can validate successfully. -->
<xsl:param name="bookTitle">Untitled Book</xsl:param>
<xsl:param name="bookAuthor">Unknown Author</xsl:param>
<xsl:param name="bookYear">1900</xsl:param>

<!-- this list is unfortunately needed because XSLT has no proper date processing capabilities -->
<months:months>
  <name num="Jan">01</name>
  <name num="Feb">02</name>
  <name num="Mar">03</name>
  <name num="Apr">04</name>
  <name num="May">05</name>
  <name num="Jun">06</name>
  <name num="Jul">07</name>
  <name num="Aug">08</name>
  <name num="Sep">09</name>
  <name num="Oct">10</name>
  <name num="Nov">11</name>
  <name num="Dec">12</name>
</months:months>

<xsl:variable name="vMonths" select="document('')/*/months:*" />

<xsl:template match="/rss/channel">
	<xsl:element name="medius:book">
		<xsl:attribute name="title"><xsl:value-of select="$bookTitle"/></xsl:attribute>
		<xsl:attribute name="author"><xsl:value-of select="$bookAuthor"/></xsl:attribute>
		<xsl:attribute name="year"><xsl:value-of select="$bookYear"/></xsl:attribute>
		<xsl:element name="medius:chapter">
			<xsl:attribute name="title">Chapter 1</xsl:attribute>
			<xsl:attribute name="orderIndex">1</xsl:attribute>
			<xsl:for-each select='item[wp:post_type="post" and wp:status="publish"]'>
				<xsl:sort select="substring(pubDate, 13, 4)" data-type="number" order="ascending"/>
				<xsl:sort select="$vMonths/name[@num=substring(current()/pubDate, 9, 3)]" data-type="number" order="ascending"/>
				<xsl:sort select="substring(pubDate, 6, 2)" data-type="number" order="ascending"/>		
				<xsl:element name="medius:post">
					<xsl:attribute name="title">
						<xsl:value-of select="title"/>
					</xsl:attribute>
					<xsl:attribute name="author">
						<xsl:value-of select="dc:creator"/>
					</xsl:attribute>
					<xsl:attribute name="publishDate">
			  <!-- build ISO 8601 combined date/time string -->
						<xsl:value-of select="substring(pubDate, 13, 4)"/>-<xsl:value-of select="$vMonths/name[@num=substring(current()/pubDate, 9, 3)]"/>-<xsl:value-of select="substring(pubDate, 6, 2)"/>T<xsl:value-of select="substring(pubDate, 18, 8)"/>Z</xsl:attribute>
					<!-- we want this in a CDATA section to prevent breaking later XML (same reason it is CDATA in the original!) -->
					<xsl:text disable-output-escaping="yes">&lt;![CDATA[</xsl:text> 
					<xsl:value-of select="content:encoded" disable-output-escaping="yes" />
					<xsl:text disable-output-escaping="yes">]]</xsl:text>
					<xsl:text disable-output-escaping="yes">&gt;</xsl:text>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:element>
</xsl:template>
</xsl:transform>